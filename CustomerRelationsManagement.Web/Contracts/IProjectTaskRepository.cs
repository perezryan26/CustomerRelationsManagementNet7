using CustomerRelationsManagement.Web.Data;

namespace CustomerRelationsManagement.Web.Contracts
{
    public interface IProjectTaskRepository : IGenericRepository<ProjectTask>
    {
        //Task<ProjectTask> GetAsync(int? taskId);
    }
}
