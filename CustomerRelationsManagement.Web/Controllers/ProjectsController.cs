using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Contracts;
using AutoMapper;
using CustomerRelationsManagement.Web.Models;
using CustomerRelationsManagement.Web.Repositories;

namespace CustomerRelationsManagement.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;

        public ProjectsController(ApplicationDbContext context, IProjectRepository projectRepository, IMapper mapper)
        {
            _context = context;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var model = await projectRepository.GetAllAsync();
            return View(model);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var model = await projectRepository.GetAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            var employees = _context.Users.Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName
            }).ToList();

            var model = new ProjectCreateViewModel
            {
                Employees = employees
            };

            return View(model);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = mapper.Map<Project>(model);
                project.IsComplete = false;
                project.DateCreated = DateTime.Now;

                foreach (var employeeId in model.EmployeeIds)
                {
                    var employee = await _context.Users.FindAsync(employeeId);
                    if(employee != null)
                    {
                        project.Employees.Add(employee);
                    }
                }
                await projectRepository.AddAsync(project);
                return RedirectToAction(nameof(Index));
            }

            model.Employees = _context.Users
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                }).ToList();

            return View(model);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var model = await projectRepository.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            model.Employees = _context.Users
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                }).ToList();
            return View(model);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await projectRepository.UpdateAsync(model);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error has occured please try again");
            }
            model.Employees = _context.Users
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                }).ToList();
            return View(model);
        }


        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Positions'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
