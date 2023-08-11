﻿using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;

namespace CustomerRelationsManagement.Web.Contracts
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<ProjectViewModel> GetAsync(int? projectId);
        Task<List<ProjectViewModel>> GetAllAsync();
        Task<List<ProjectViewModel>> GetRangeAsync(string userId);
        Task UpdateAsync(ProjectViewModel model);

        
    }
}
