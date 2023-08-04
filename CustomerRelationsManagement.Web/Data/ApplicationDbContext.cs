using CustomerRelationsManagement.Web.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CustomerRelationsManagement.Web.Models;

namespace CustomerRelationsManagement.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleSeedConfiguration());
            builder.ApplyConfiguration(new UserSeedConfiguration());
            builder.ApplyConfiguration(new UserRoleSeedConfiguration());
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Deal> Deals { get; set; }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set;}
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<CustomerRelationsManagement.Web.Models.EmployeeListViewModel> EmployeeListViewModel { get; set; } = default!;
    }
}