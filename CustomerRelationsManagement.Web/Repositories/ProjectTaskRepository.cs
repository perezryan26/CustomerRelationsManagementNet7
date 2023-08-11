using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;

namespace CustomerRelationsManagement.Web.Repositories
{
    public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
