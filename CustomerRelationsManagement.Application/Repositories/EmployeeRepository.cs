using CustomerRelationsManagement.Application.Contracts;
using CustomerRelationsManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerRelationsManagement.Application.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Employee> GetAsync(string? id)
        {
            if(id == null)
            {
                return null;
            }

            return await context.Users
               .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await context.Users
                .Include(q => q.Position)
                .ToListAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            context.Update(employee);
            await context.SaveChangesAsync();
        }
    }
}
