﻿using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerRelationsManagement.Web.Repositories
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        private readonly ApplicationDbContext context;

        public AnnouncementRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Announcement>> GetRecentAnnouncements()
        {
            return await context.Announcements
                .OrderByDescending(a => a.DatePublished)
                .Take(3)
                .ToListAsync();
        }
    }
}
