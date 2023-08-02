using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;

namespace CustomerRelationsManagement.Web.Contracts
{
    public interface IDealRepository : IGenericRepository<Deal>
    {
        //Task Deal(int clientId);
        Task<DealOverviewViewModel> GetDealOverviewList();
        Task CreateDeal(DealCreateViewModel model);
        Task<DealViewModel> GetDeal(int? id);
    }
}
