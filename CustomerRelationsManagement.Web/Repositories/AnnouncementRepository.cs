using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;

namespace CustomerRelationsManagement.Web.Repositories
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
