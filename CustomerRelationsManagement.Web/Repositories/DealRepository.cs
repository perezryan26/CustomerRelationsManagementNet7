using AutoMapper;
using CustomerRelationsManagement.Web.Constants;
using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CustomerRelationsManagement.Web.Repositories
{
    public class DealRepository : GenericRepository<Deal>, IDealRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        //private readonly UserManager<Employee> userManager;
        //private readonly IClientRepository clientRepository;

        public DealRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            /*
this.userManager = userManager;
this.clientRepository = clientRepository;
*/
        }

        public async Task CreateDeal(DealCreateViewModel model)
        {
            var deal = mapper.Map<Deal>(model);
            await AddAsync(deal);
        }

        public async Task<DealOverviewViewModel> GetDealOverviewList()
        {
            var deals = await context.Deals.Include(q => q.Client).ToListAsync();
            var model = new DealOverviewViewModel
            {
                Deals = mapper.Map<List<DealViewModel>>(deals)
            };

            return model;
        }

        public async Task<DealViewModel> GetDeal(int? id)
        {
            if (id == null || context.Deals == null)
            {
                return null;
            }

            var deal = await context.Deals
                .Include(q => q.Client)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (deal == null)
            {
                return null;
            }

            var model = mapper.Map<DealViewModel>(deal);
            return model;
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
