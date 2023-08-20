using AutoMapper;
using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace CustomerRelationsManagement.Web.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Employee> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProjectRepository(ApplicationDbContext context, IMapper mapper, UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProjectViewModel> GetAsync(int? projectId)
        {
            var project = await context.Projects
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if(project == null)
            {
                return null;
            }

           var model = mapper.Map<ProjectViewModel>(project);
           model.EmployeeIds = project.Employees.Select(e => e.Id).ToList();

           return model;
        }

        public async Task<List<ProjectViewModel>> GetRangeAsync(string userId)
        {
            var model = mapper.Map<List<ProjectViewModel>>(context.Projects.Where(p => p.Employees.Any(e => e.Id == userId)).ToList());
            return model;
        }

        public async Task<List<ProjectViewModel>> GetAllAsync()
        {
            var projects = await context.Projects
                .Include(p => p.Employees)
                .ToListAsync();

            var model = mapper.Map<List<ProjectViewModel>>(projects);
            return model;
        }

        public async Task UpdateAsync(ProjectViewModel model)
        {
            var newEmployeeList = new List<Employee>();
            foreach (var employeeId in model.EmployeeIds)
            {
                var employee = await context.Users.FindAsync(employeeId);

                if (employee != null)
                {
                    newEmployeeList.Add(employee);
                }
            }

            var project = await context.Projects
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            project.DateModified = DateTime.Now;
            project.IsComplete = model.IsComplete;
            project.Name = model.Name;
            project.Description = model.Description;
            project.Employees = newEmployeeList;

            context.Update(project);
            await context.SaveChangesAsync();
        }

        public async Task<List<EmployeeViewModel>> GetEmployeesAsync()
        {
            var Employees = context.Users
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                }).ToList();

            var model = mapper.Map<List<EmployeeViewModel>>(Employees);

            return model;
        }
    }
}
