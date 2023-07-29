using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;

namespace CustomerRelationsManagement.Web.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(ApplicationDbContext context) : base(context) 
        { 
        }
    }
}
