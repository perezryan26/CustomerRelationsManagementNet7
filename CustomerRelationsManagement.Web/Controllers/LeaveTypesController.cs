using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Application.Contracts;
using AutoMapper;
using CustomerRelationsManagement.Common.Models;
using Microsoft.AspNetCore.Authorization;
using CustomerRelationsManagement.Common.Constants;

namespace CustomerRelationsManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly ILogger<LeaveTypesController> logger;

        public LeaveTypesController(ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, ILogger<LeaveTypesController> logger)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.logger = logger;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            return View(mapper.Map<List<LeaveTypeViewModel>>(await leaveTypeRepository.GetAllAsync()));
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Task not found.");
            }

            try
            {
                var model = mapper.Map<LeaveTypeViewModel>(await leaveTypeRepository.GetAsync(id));

                if (model == null)
                {
                    return NotFound("Leave type not found.");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while viewing details for the leave type.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while viewing details for the leave type.");
            }
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeViewModel leaveTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var leaveType = mapper.Map<LeaveType>(leaveTypeViewModel);
                    await leaveTypeRepository.AddAsync(leaveType);

                    TempData["SuccessMessage"] = "Leave type created successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the leave type.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the leave type.");
            }

            return View(leaveTypeViewModel);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = mapper.Map<LeaveTypeViewModel>(await leaveTypeRepository.GetAsync(id));

            if (model == null)
            {
                return NotFound("Leave type not found.");
            }

            return View(model);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeViewModel leaveTypeViewModel)
        {
            if (id != leaveTypeViewModel.Id)
            {
                return BadRequest("IDs do not match.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var leaveType = await leaveTypeRepository.GetAsync(id);

                    if(leaveType == null)
                    {
                        return NotFound("Leave Type not found.");
                    }
                    
                    mapper.Map(leaveTypeViewModel, leaveType);
                    await leaveTypeRepository.UpdateAsync(leaveType);

                    TempData["SuccessMessage"] = "Leave Type updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await leaveTypeRepository.Exists(leaveTypeViewModel.Id))
                    {
                        return NotFound("Leave Type not found.");
                    }
                    else
                    {
                        return Conflict("Concurrency conflict. The leave type has been modified by another user.");
                    }
                }
                catch (Exception ex) 
                {
                    logger.LogError(ex, "An error occurred while saving the leave type changes.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the leave type changes.");
                } 
            }
            return View(leaveTypeViewModel);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var leaveType = await leaveTypeRepository.GetAsync(id);

                if (leaveType == null)
                {
                    return NotFound("Leave Type not found.");
                }

                await leaveTypeRepository.DeleteAsync(id);

                TempData["SuccessMessage"] = "Leave Type deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting the leave type.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the leave type.");
            }


            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(int id)
        {
            try
            {
                var leaveType = await leaveTypeRepository.GetAsync(id);

                if (leaveType == null)
                {
                    return NotFound("Leave Type not found.");
                }

                await leaveAllocationRepository.LeaveAllocation(id);

                TempData["SuccessMessage"] = "Leave Type successfully allocated to all employees";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while allocating the leave type.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while allocating the leave type.");
            }

            
        }
    }
}