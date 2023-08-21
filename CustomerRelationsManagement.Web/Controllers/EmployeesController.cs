using AutoMapper;
using CustomerRelationsManagement.Common.Constants;
using CustomerRelationsManagement.Application.Contracts;
using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Common.Models;
using CustomerRelationsManagement.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CustomerRelationsManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class EmployeesController : Controller
    {
        private readonly UserManager<Employee> userManager;
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IPositionRepository positionRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ILogger<EmployeesController> logger;
        private readonly ApplicationDbContext context;

        public EmployeesController(UserManager<Employee> userManager, IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IPositionRepository positionRepository,
            IEmployeeRepository employeeRepository,
            ILogger<EmployeesController> logger,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.positionRepository = positionRepository;
            this.employeeRepository = employeeRepository;
            this.logger = logger;
            this.context = context;
        }

        // GET: EmployeesController.
        public async Task<IActionResult> Index()
        {
            try
            {
                var employees = await employeeRepository.GetAllAsync();

                var model = mapper.Map<List<EmployeeListViewModel>>(employees);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving employees.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving employees.");
            }
        }

        // GET: EmployeesController/ViewAllocations/employeeId
        public async Task<ActionResult> ViewAllocations(string id)
        {
            try
            {
                var model = await leaveAllocationRepository.GetEmployeeAllocations(id);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving employee allocations.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving employee allocations.");
            }
        }

        public async Task<ActionResult> AssignPosition(string id)
        {
            var positions = await positionRepository.GetAllAsync();
            var employee = await employeeRepository.GetAsync(id);

            if(employee == null)
            {
                return NotFound(); 
            }  

            ViewBag.EmployeeId = id;
            ViewBag.Positions = new SelectList(positions, "Id", "Name", employee.PositionId);

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignPosition(string id, int positionId)
        {
            try
            {
                var employee = await employeeRepository.GetAsync(id);

                if (employee == null)
                {
                    return NotFound();
                }

                employee.PositionId = positionId;
                await employeeRepository.UpdateEmployeeAsync(employee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error has occured when assigning a position, please try again.");
                ModelState.AddModelError(string.Empty, "An error has occured when assigning a position, please try again.");
            }

            var positions = await positionRepository.GetAllAsync();

            ViewBag.EmployeeId = id;
            ViewBag.Positions = new SelectList(positions, "Id", "Name", positionId);

            return View();
        }

        public async Task<ActionResult> AssignDepartment(string id)
        {
            var employee = await employeeRepository.GetAsync(id);

            if (employee == null)
            {
                return NotFound(); 
            }

            var departments = typeof(DepartmentType).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetValue(null).ToString())
                .ToList();

            ViewBag.EmployeeId = id;
            ViewBag.Departments = new SelectList(departments, employee.Department);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignDepartment(string id, string department)
        {
            var employee = await employeeRepository.GetAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var departments = typeof(DepartmentType).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetValue(null).ToString())
                .ToList();

            if (!departments.Contains(department))
            {
                ViewBag.EmployeeId = id;
                ViewBag.Departments = new SelectList(departments, employee.Department);
                return View(departments);
            }

            try
            {
                employee.Department = department;
                await employeeRepository.UpdateEmployeeAsync(employee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error has occured when assigning a department, please try again.");
                ModelState.AddModelError(string.Empty, "An error has occured when assigning a department, please try again.");
            }

            ViewBag.EmployeeId = id;
            ViewBag.Departments = new SelectList(departments, employee.Department);
            return View();
        }

        // GET: EmployeesController/EditAllocations/5
        public async Task<ActionResult> EditAllocation(int id)
        {
            var model = await leaveAllocationRepository.GetEmployeeAllocation(id);

            if (model == null)
            {
                return NotFound("Employee allocation not found.");
            }
            return View(model);
        }


        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAllocation(int id, LeaveAllocationEditViewModel model)
        {  
            if (ModelState.IsValid)
            {
                try
                {
                    if (await leaveAllocationRepository.UpdateEmployeeAllocation(model))
                    {
                        TempData["SuccessMessage"] = "Employee allocation updated successfully.";
                        return RedirectToAction(nameof(ViewAllocations), new { id = model.EmployeeId });
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await leaveAllocationRepository.Exists(model.Id))
                    {
                        return NotFound("Employee allocation not found.");
                    }
                    else
                    {
                        return Conflict("Concurrency conflict. The employee allocation has been modified by another user.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error has occured when editing an employee allocation");
                    ModelState.AddModelError(string.Empty, "An error has occured when editing an employee allocation");
                }
            }
            model.Employee = mapper.Map<EmployeeListViewModel>(await userManager.FindByIdAsync(model.EmployeeId));
            model.LeaveType = mapper.Map<LeaveTypeViewModel>(await leaveTypeRepository.GetAsync(model.LeaveTypeId));
            return View(model);
        }
    }
}
