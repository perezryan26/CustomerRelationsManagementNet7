using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Common.Models;
using CustomerRelationsManagement.Application.Contracts;

namespace CustomerRelationsManagement.Application.Contracts
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task LeaveAllocation(int leaveTypeId);
        Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period);
        Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string employeeId);
        Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int leaveTypeId);
        Task<LeaveAllocationEditViewModel> GetEmployeeAllocation(int id);
        Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditViewModel model);
    }
}
