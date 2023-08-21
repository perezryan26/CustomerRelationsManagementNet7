using AutoMapper;
using CustomerRelationsManagement.Application.Contracts;
using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerRelationsManagement.Application.Repositories
{
    public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProjectTaskRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ProjectTask>> GetRangeAsync(int projectId)
        {
            return await context.ProjectTasks.Where(q => q.ProjectId == projectId).ToListAsync();
        }
    }
}
