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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CustomerRelationsManagement.Web.Constants;

namespace CustomerRelationsManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ProjectsController> logger;

        public ProjectsController(ApplicationDbContext context, IProjectRepository projectRepository, IMapper mapper, ILogger<ProjectsController> logger)
        {
            _context = context;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await projectRepository.GetAllAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving projects.\"");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving projects.");
            }
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Project not found.");
            }

            try
            {
                var model = await projectRepository.GetAsync(id);

                if (model == null)
                {
                    return NotFound("Project not found.");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while viewing details for the project.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while viewing details for the project.");
            }
        }

        // GET: Projects/Create
        public async Task<IActionResult> Create()
        {
            var employees = await projectRepository.GetEmployeesAsync();

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
            try
            {
                if (ModelState.IsValid)
                {
                    var project = mapper.Map<Project>(model);
                    project.IsComplete = false;
                    project.DateCreated = DateTime.Now;

                    foreach (var employeeId in model.EmployeeIds)
                    {
                        var employee = await _context.Users.FindAsync(employeeId);
                        if (employee != null)
                        {
                            project.Employees.Add(employee);
                        }
                    }

                    await projectRepository.AddAsync(project);

                    TempData["SuccessMessage"] = "Project created successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the project.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the project.");
            }

            model.Employees = await projectRepository.GetEmployeesAsync();
            return View(model);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await projectRepository.GetAsync(id);

            if (model == null)
            {
                return NotFound("Project not found.");
            }

            model.Employees = await projectRepository.GetEmployeesAsync();
            return View(model);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("IDs do not match.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var project = await projectRepository.GetAsync(id);

                    if (project == null)
                    {
                        return NotFound("Project not found.");
                    }

                    mapper.Map(model, project);
                    await projectRepository.UpdateAsync(project);

                    TempData["SuccessMessage"] = "Project updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await projectRepository.Exists(model.Id))
                    {
                        return NotFound("Project not found.");
                    }
                    else
                    {
                        return Conflict("Concurrency conflict. The project has been modified by another user.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while saving the project changes.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the project changes.");
                }
            }
            model.Employees = await projectRepository.GetEmployeesAsync();
            return View(model);
        }


        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var project = await projectRepository.GetAsync(id);

                if (project == null)
                {
                    return NotFound("Project not found.");
                }

                await projectRepository.DeleteAsync(id);

                TempData["SuccessMessage"] = "Project deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting the project.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the project.");
            }
        }
    }
}
