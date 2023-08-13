using AutoMapper;
using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerRelationsManagement.Web.Repositories
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
    }
}
