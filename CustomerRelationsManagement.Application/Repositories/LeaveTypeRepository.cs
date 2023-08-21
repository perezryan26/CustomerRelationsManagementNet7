using CustomerRelationsManagement.Application.Contracts;
using CustomerRelationsManagement.Data;

namespace CustomerRelationsManagement.Application.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(ApplicationDbContext context) : base(context) 
        { 
        }
    }
}
