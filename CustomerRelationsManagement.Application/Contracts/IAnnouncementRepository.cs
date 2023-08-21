using CustomerRelationsManagement.Application.Contracts;
using CustomerRelationsManagement.Data;

namespace CustomerRelationsManagement.Application.Contracts
{
    public interface IAnnouncementRepository : IGenericRepository<Announcement>
    {
        Task<List<Announcement>> GetRecentAnnouncements();
    }
}
