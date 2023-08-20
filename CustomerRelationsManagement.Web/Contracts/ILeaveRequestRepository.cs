using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;

namespace CustomerRelationsManagement.Web.Contracts
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
