using CustomerRelationsManagement.Web.Data;

namespace CustomerRelationsManagement.Web.Contracts
{
    public interface IProjectTaskRepository : IGenericRepository<ProjectTask>
    {
        Task<List<ProjectTask>> GetRangeAsync(int projectId);
    }
}
