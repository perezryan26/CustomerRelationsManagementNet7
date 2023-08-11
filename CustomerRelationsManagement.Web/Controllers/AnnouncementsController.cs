using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerRelationsManagement.Web.Data;
using AutoMapper;
using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Models;
using CustomerRelationsManagement.Web.Repositories;

namespace CustomerRelationsManagement.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;
        private readonly IAnnouncementRepository announcementRepository;

        public AnnouncementsController(ApplicationDbContext context, IMapper mapper, IAnnouncementRepository announcementRepository)
        {
            _context = context;
            this.mapper = mapper;
            this.announcementRepository = announcementRepository;
        }

        // GET: Announcements
        public async Task<IActionResult> Index()
        {
            var model = mapper.Map<List<AnnouncementViewModel>>(await announcementRepository.GetAllAsync());
            return View(model);
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var model = mapper.Map<AnnouncementViewModel>(await announcementRepository.GetAsync(id));

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Announcements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnnouncementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var announcement = mapper.Map<Announcement>(model);
                announcement.DatePublished = DateTime.Now;
                await announcementRepository.AddAsync(announcement);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var model = mapper.Map<AnnouncementViewModel>(await announcementRepository.GetAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnnouncementViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await announcementRepository.UpdateAsync(mapper.Map<Announcement>(model));
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error has occured please try again");
            }
            return View(model);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Announcements == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Announcements'  is null.");
            }
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementExists(int id)
        {
          return (_context.Announcements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
