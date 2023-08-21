using CustomerRelationsManagement.Application.Contracts;
using CustomerRelationsManagement.Data;

namespace CustomerRelationsManagement.Application.Repositories
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
