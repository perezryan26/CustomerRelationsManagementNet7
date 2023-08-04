using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;

namespace CustomerRelationsManagement.Web.Repositories
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
