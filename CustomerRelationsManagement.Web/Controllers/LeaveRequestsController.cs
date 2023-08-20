using AutoMapper;
using CustomerRelationsManagement.Web.Constants;
using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerRelationsManagement.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILogger<LeaveRequestsController> logger;

        public LeaveRequestsController(ApplicationDbContext context, ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, ILogger<LeaveRequestsController> logger
            )
        {
            _context = context;
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.logger = logger;
        }

        // GET: LeaveRequests
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await leaveRequestRepository.GetAdminLeaveRequestList();
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving the leave requests.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the leave requests.");
            }
        }

        public async Task<ActionResult> MyLeave()
        {
            try
            {
                var model = await leaveRequestRepository.GetMyLeaveDetails();
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving the leave requests.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the leave requests.");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                await leaveRequestRepository.ChangeApprovalStatus(id, approved);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while approving the leave request.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while approving the the leave request.");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Leave request not found.");
            }

            try
            {
                var model = await leaveRequestRepository.GetLeaveRequestAsync(id);

                if (model == null)
                {
                    return NotFound("Leave request not found.");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while viewing details for the leave request.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while viewing details for the leave request.");
            }
        }

        // GET: LeaveRequests/Create
        public async Task<IActionResult> Create()
        {
            var leaveTypes = await leaveTypeRepository.GetAllAsync();
            var model = new LeaveRequestCreateViewModel
            {
                LeaveTypes = new SelectList(leaveTypes, "Id", "Name"),
            };

            return View(model);
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await leaveRequestRepository.CreateLeaveRequest(model))
                    {
                        return RedirectToAction(nameof(MyLeave));
                    }
                    ModelState.AddModelError(string.Empty, "You have exceeded your allocation with this request.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the leave request.");
                ModelState.AddModelError(string.Empty, "An error has occured please try again");
            }

            var leaveTypes = await leaveTypeRepository.GetAllAsync();
            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name", model.LeaveTypeId);

            return View(model);
        }

        // GET: LeaveRequests/Edit/5
        /*
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
            return View(leaveRequest);
        }
        */

        // POST: LeaveRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,LeaveTypeId,DateRequested,RequestComments,Approved,Cancelled,RequestingEmployeeId,Id,DateCreated,DateModified")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await leaveRequestRepository.Exists(leaveRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
            return View(leaveRequest);
        }
        */

        // POST: LeaveRequests/Delete/5
        /*
        [Authorize(Roles = Roles.Administrator)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var leaveRequest = await leaveRequestRepository.GetAsync(id);

                if (leaveRequest == null)
                {
                    return NotFound("Leave request not found.");
                }

                await leaveRequestRepository.DeleteAsync(id);

                TempData["SuccessMessage"] = "Leave request deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the leave request.");
            }
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await leaveRequestRepository.CancelLeaveRequest(id);

                TempData["SuccessMessage"] = "Leave request cancelled successfully.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while cancelling the leave request.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while cancelling the leave request.");
            }
            return RedirectToAction(nameof(MyLeave));
        }
    }
}
