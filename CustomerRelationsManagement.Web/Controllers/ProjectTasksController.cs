using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;

namespace CustomerRelationsManagement.Web.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProjectTaskViewModel.Include(p => p.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjectTaskViewModel == null)
            {
                return NotFound();
            }

            var projectTaskViewModel = await _context.ProjectTaskViewModel
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTaskViewModel == null)
            {
                return NotFound();
            }

            return View(projectTaskViewModel);
        }

        // GET: ProjectTasks/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,IsComplete,Name,Description,ProjectId,TaskPriority,DateDue")] ProjectTaskViewModel projectTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTaskViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", projectTaskViewModel.ProjectId);
            return View(projectTaskViewModel);
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProjectTaskViewModel == null)
            {
                return NotFound();
            }

            var projectTaskViewModel = await _context.ProjectTaskViewModel.FindAsync(id);
            if (projectTaskViewModel == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", projectTaskViewModel.ProjectId);
            return View(projectTaskViewModel);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,IsComplete,Name,Description,ProjectId,TaskPriority,DateDue")] ProjectTaskViewModel projectTaskViewModel)
        {
            if (id != projectTaskViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTaskViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskViewModelExists(projectTaskViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", projectTaskViewModel.ProjectId);
            return View(projectTaskViewModel);
        }

        // GET: ProjectTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectTaskViewModel == null)
            {
                return NotFound();
            }

            var projectTaskViewModel = await _context.ProjectTaskViewModel
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTaskViewModel == null)
            {
                return NotFound();
            }

            return View(projectTaskViewModel);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjectTaskViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProjectTaskViewModel'  is null.");
            }
            var projectTaskViewModel = await _context.ProjectTaskViewModel.FindAsync(id);
            if (projectTaskViewModel != null)
            {
                _context.ProjectTaskViewModel.Remove(projectTaskViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTaskViewModelExists(int id)
        {
          return (_context.ProjectTaskViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
