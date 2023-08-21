using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Application.Contracts;

namespace CustomerRelationsManagement.Application.Contracts
{
    public interface IProjectTaskRepository : IGenericRepository<ProjectTask>
    {
        Task<List<ProjectTask>> GetRangeAsync(int projectId);
    }
}
