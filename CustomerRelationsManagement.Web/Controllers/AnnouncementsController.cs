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
using Microsoft.AspNetCore.Identity;

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
            try
            {
                var model = mapper.Map<List<AnnouncementViewModel>>(await announcementRepository.GetAllAsync());
                return View(model);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving announcements.");
            }
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Announcement not found.");
            }

            try
            {
                var model = mapper.Map<AnnouncementViewModel>(await announcementRepository.GetAsync(id));

                if (model == null)
                {
                    return NotFound("Announcement not found.");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while viewing details for the announcement.");
            }
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
            try
            {
                if (ModelState.IsValid)
                {
                    var announcement = mapper.Map<Announcement>(model);
                    announcement.DatePublished = DateTime.Now;
                    await announcementRepository.AddAsync(announcement);

                    TempData["SuccessMessage"] = "Announcement created successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the changes.");
            }
            return View(model);
        }

        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = mapper.Map<AnnouncementViewModel>(await announcementRepository.GetAsync(id));
            
            if (model == null)
            {
                return NotFound("Announcement not found.");
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
            if (id != model.Id)
            {
                return BadRequest("IDs do not match.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await announcementRepository.UpdateAsync(mapper.Map<Announcement>(model));

                    TempData["SuccessMessage"] = "Announcement updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await announcementRepository.Exists(model.Id))
                    {
                        return NotFound("Announcement not found.");
                    }
                    else
                    {
                        return Conflict("Concurrency conflict. The announcement has been modified by another user.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the changes.");
                }
            }
            return View(model);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var announcement = await announcementRepository.GetAsync(id);

                if (announcement == null)
                {
                    return NotFound("Task not found.");
                }

                await announcementRepository.DeleteAsync(id);

                TempData["SuccessMessage"] = "Announcement deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the announcement.");
            }
        }
    }
}
