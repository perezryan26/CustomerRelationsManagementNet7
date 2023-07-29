using CustomerRelationsManagement.Web.Constants;
using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;

namespace CustomerRelationsManagement.Web.Repositories
{
    public class DealRepository : GenericRepository<Deal>, IDealRepository
    {
        //private readonly UserManager<Employee> userManager;
        //private readonly IClientRepository clientRepository;

        public DealRepository(ApplicationDbContext context/*,
            UserManager<Employee> userManager, IClientRepository clientRepository*/) : base(context)
        {
            /*
            this.userManager = userManager;
            this.clientRepository = clientRepository;
            */
        }

        /*
        public async Task Deal(int clientId)
        {
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var client = await clientRepository.GetAsync(clientId);
            var deals = new List<Deal>();

            foreach(var employee in employees)
            {
                deals.Add( new Deal
                {
                    EmployeeId = employee.Id,
                    ClientId = clientId
                });
            }

            await AddRangeAsync(deals);
        }
        */
    }
}
