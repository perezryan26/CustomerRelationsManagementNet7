using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;
using AutoMapper;
using CustomerRelationsManagement.Web.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CustomerRelationsManagement.Web.Controllers
{
    [Authorize]
    public class ProjectTasksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProjectTaskRepository projectTaskRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<Employee> userManager;
        private readonly ILogger<ProjectTasksController> logger;

        public ProjectTasksController(IMapper mapper, IProjectTaskRepository projectTaskRepository, IHttpContextAccessor httpContextAccessor, UserManager<Employee> userManager, ILogger<ProjectTasksController> logger)
        {
            this.mapper = mapper;
            this.projectTaskRepository = projectTaskRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.logger = logger;
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Task not found.");
            }

            try
            {
                var model = mapper.Map<ProjectTaskViewModel>(await projectTaskRepository.GetAsync(id));

                if (model == null)
                {
                    return NotFound("Task not found.");
                }

                model.Employee = mapper.Map<EmployeeListViewModel>(await userManager.FindByIdAsync(model.EmployeeId));

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while viewing details for the task.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while viewing details for the task.");
            }
        }

        // GET: ProjectTasks/Create
        public IActionResult Create(int projectId)
        {
            var projectTask = new ProjectTaskCreateViewModel
            {
                ProjectId = projectId
            };
            return View(projectTask);
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTaskCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);

                    if (user == null)
                    {
                        return BadRequest("User not found.");
                    }

                    var projectTask = mapper.Map<ProjectTask>(model);
                    projectTask.EmployeeId = user.Id;

                    await projectTaskRepository.AddAsync(projectTask);

                    TempData["SuccessMessage"] = "Task created successfully.";
                    return RedirectToAction("ProjectDetails", "Home", new { projectId = projectTask.ProjectId });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the project.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the project.");
            }

            return View(model);
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = mapper.Map<ProjectTaskEditViewModel>(await projectTaskRepository.GetAsync(id));
            
            if (model == null)
            {
                return NotFound("Task not found.");
            }

            return View(model);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectTaskEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("IDs do not match.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var projectTask = await projectTaskRepository.GetAsync(id);

                    if (projectTask == null)
                    {
                        return NotFound("Task not found.");
                    }

                    /*
                    projectTask.Name = model.Name;
                    projectTask.Description = model.Description;
                    projectTask.DateDue = (DateTime)model.DateDue;
                    projectTask.TaskPriority = model.TaskPriority;
                    projectTask.IsComplete = model.IsComplete;
                    */

                    mapper.Map(model, projectTask);
                    await projectTaskRepository.UpdateAsync(projectTask);

                    TempData["SuccessMessage"] = "Task updated successfully.";
                    return RedirectToAction("ProjectDetails", "Home", new { projectId = model.ProjectId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await projectTaskRepository.Exists(model.Id))
                    {
                        return NotFound("Task not found.");
                    }
                    else
                    {
                        return Conflict("Concurrency conflict. The task has been modified by another user.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while saving the task changes.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the task changes.");
                }
            }
            return View(model);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        
        {
            try
            {
                var projectTask = await projectTaskRepository.GetAsync(id);
                
                if (projectTask == null)
                {
                    return NotFound("Task not found.");
                }

                await projectTaskRepository.DeleteAsync(id);

                TempData["SuccessMessage"] = "Task deleted successfully.";
                return RedirectToAction("ProjectDetails", "Home", new { projectId = projectTask?.ProjectId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting the task.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the task.");
            }
        }
    }
}
