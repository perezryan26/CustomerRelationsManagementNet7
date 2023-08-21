using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerRelationsManagement.Application.Contracts;
using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomerRelationsManagement.Application.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<Employee> userManager;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly AutoMapper.IConfigurationProvider configurationProvider;
        private readonly IEmailSender emailSender;

        public LeaveRequestRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<Employee> userManager, ILeaveAllocationRepository leaveAllocationRepository, AutoMapper.IConfigurationProvider configurationProvider, IEmailSender emailSender) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.configurationProvider = configurationProvider;
            this.emailSender = emailSender;
        }

        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await GetAsync(leaveRequestId);

            if (leaveRequest != null)
            {
                leaveRequest.Cancelled = true;
                await UpdateAsync(leaveRequest);

                var user = await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);
                
                await emailSender.SendEmailAsync(user.Email, $"Leave Request Cancelled", $"Your leave request from " +
                    $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been Cancelled Successfully.");

            }
        }

        public async Task ChangeApprovalStatus(int leaveRequestId, bool approved)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Approved = approved;

            if (approved)
            {
                var allocation = await leaveAllocationRepository.GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays -= daysRequested;

                await leaveAllocationRepository.UpdateAsync(allocation);
            }

            await UpdateAsync(leaveRequest);

            var user = await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);
            var approvalStatus = approved ? "Approved" : "Declined";

            await emailSender.SendEmailAsync(user.Email, $"Leave Request {approvalStatus}", $"Your leave request from " +
                $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been {approvalStatus}");
        }

        public async Task<bool> CreateLeaveRequest(LeaveRequestCreateViewModel model)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);

            var leaveAllocation = await leaveAllocationRepository.GetEmployeeAllocation(user.Id, model.LeaveTypeId);

            if (leaveAllocation == null)
            {
                return false;
            }

            var daysRequested = (int)(model.EndDate.Value - model.StartDate.Value).TotalDays;

            if (daysRequested > leaveAllocation.NumberOfDays)
            {

                return false;
            }

            var leaveRequest = mapper.Map<LeaveRequest>(model);
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.RequestingEmployeeId = user.Id;

            await AddAsync(leaveRequest);

            await emailSender.SendEmailAsync(user.Email, "Leave Request Submitted Successfully", $"Your leave request from " +
                $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been submitted for approval");

            return true;
        }

        public async Task<AdminLeaveRequestViewModel> GetAdminLeaveRequestList()
        {
            var leaveRequests = await context.LeaveRequests.Include(q => q.LeaveType).ToListAsync();
            var model = new AdminLeaveRequestViewModel
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
                PendingRequests = leaveRequests.Count(q => q.Approved == null),
                RejectedRequests = leaveRequests.Count(q => q.Approved == false),
                LeaveRequests = mapper.Map<List<LeaveRequestViewModel>>(leaveRequests)
            };

            foreach (var leaveRequest in model.LeaveRequests)
            {
                leaveRequest.Employee = mapper.Map<EmployeeListViewModel>(await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId));
            }

            return model;
        }

        public async Task<List<LeaveRequestViewModel>> GetAllAsync(string employeeId)
        {
            return await context.LeaveRequests.Where(q => q.RequestingEmployeeId == employeeId)
                .ProjectTo<LeaveRequestViewModel>(configurationProvider)
                .ToListAsync();
        }

        public async Task<LeaveRequestViewModel> GetLeaveRequestAsync(int? id)
        {
            var leaveRequest = await context.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (leaveRequest == null)
            {
                return null;
            }

            var model = mapper.Map<LeaveRequestViewModel>(leaveRequest);
            model.Employee = mapper.Map<EmployeeListViewModel>(await userManager.FindByIdAsync(leaveRequest?.RequestingEmployeeId));

            return model;
        }

        public async Task<EmployeeLeaveRequestViewModel> GetMyLeaveDetails()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var allocations = (await leaveAllocationRepository.GetEmployeeAllocations(user.Id)).LeaveAllocations;
            var requests = await GetAllAsync(user.Id);

            var model = new EmployeeLeaveRequestViewModel(allocations, requests);

            return model;
        }
    }
}
