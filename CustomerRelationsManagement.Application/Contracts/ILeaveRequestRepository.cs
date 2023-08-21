using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Common.Models;
using CustomerRelationsManagement.Application.Contracts;

namespace CustomerRelationsManagement.Application.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<bool> CreateLeaveRequest(LeaveRequestCreateViewModel model);
        Task<EmployeeLeaveRequestViewModel> GetMyLeaveDetails();
        Task<LeaveRequestViewModel> GetLeaveRequestAsync(int? id);
        Task<List<LeaveRequestViewModel>> GetAllAsync(string employeeId);
        Task<AdminLeaveRequestViewModel> GetAdminLeaveRequestList();
        Task ChangeApprovalStatus(int leaveRequestId, bool approved);
        Task CancelLeaveRequest(int leaveRequestId);

    }
}
