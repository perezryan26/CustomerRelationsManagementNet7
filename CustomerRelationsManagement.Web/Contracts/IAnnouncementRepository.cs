﻿using CustomerRelationsManagement.Web.Data;

namespace CustomerRelationsManagement.Web.Contracts
{
    public interface IAnnouncementRepository : IGenericRepository<Announcement>
    {
        Task<List<Announcement>> GetRecentAnnouncements();
    }
}