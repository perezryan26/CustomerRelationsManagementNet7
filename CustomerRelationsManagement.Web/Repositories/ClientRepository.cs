using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;

namespace CustomerRelationsManagement.Web.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
