using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Common.Models;
using CustomerRelationsManagement.Application.Contracts;

namespace CustomerRelationsManagement.Application.Contracts
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<ProjectViewModel> GetProjectAsync(int? projectId);
        Task<List<ProjectViewModel>> GetRangeAsync(string userId);
        Task<List<ProjectViewModel>> GetAllAsync();
        Task UpdateAsync(ProjectViewModel model);
        Task<List<EmployeeViewModel>> GetEmployeesAsync();

    }

}
