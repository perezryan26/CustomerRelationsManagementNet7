using AutoMapper;
using CustomerRelationsManagement.Web.Constants;
using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;
using CustomerRelationsManagement.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CustomerRelationsManagement.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<Employee> userManager;
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ApplicationDbContext context;

        public EmployeesController(UserManager<Employee> userManager, IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.context = context;
        }

        // GET: EmployeesController.
        public async Task<IActionResult> Index()
        {
            //var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var employees = await context.Users
                .Include(q => q.Position)
                .ToListAsync();

            var model = mapper.Map<List<EmployeeListViewModel>>(employees);
            return View(model);
        }

        // GET: EmployeesController/ViewAllocations/employeeId
        public async Task<ActionResult> ViewAllocations(string id)
        {
            var model = await leaveAllocationRepository.GetEmployeeAllocations(id);
            return View(model);
        }

        // GET: EmployeesController/EditAllocations/5
        public async Task<ActionResult> EditAllocation(int id)
        {
            var model = await leaveAllocationRepository.GetEmployeeAllocation(id);
            if(model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        public async Task<ActionResult> AssignDepartment(string id)
        {
            var model = await context.Users
               .FirstOrDefaultAsync(q => q.Id == id);

            //var model = await leaveAllocationRepository.GetEmployeeAllocation(id);
            if (model == null)
            {
                return NotFound();
            }

            var departments = typeof(DepartmentType).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetValue(null).ToString())
                .ToList();

            ViewBag.EmployeeId = id;
            ViewBag.Departments = departments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignDepartment(string id, string department)
        {

            try
            {
                var model = await context.Users
                    .FirstOrDefaultAsync(q => q.Id == id);
                model.Department = department;
                context.Update(model);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error has occured please try again");
            }

            var departments = typeof(DepartmentType).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetValue(null).ToString())
                .ToList();

            ViewBag.EmployeeId = id;
            ViewBag.Departments = departments;
            return View();
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAllocation(int id, LeaveAllocationEditViewModel model)
        {  
            try
            {
                if(ModelState.IsValid)
                {
                   if(await leaveAllocationRepository.UpdateEmployeeAllocation(model))
                    {
                        return RedirectToAction(nameof(ViewAllocations), new { id = model.EmployeeId });
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error has occurred. Please try again later.");
            }
            model.Employee = mapper.Map<EmployeeListViewModel>(await userManager.FindByIdAsync(model.EmployeeId));
            model.LeaveType = mapper.Map<LeaveTypeViewModel>(await leaveTypeRepository.GetAsync(model.LeaveTypeId));
            return View(model);
        }
    }
}
