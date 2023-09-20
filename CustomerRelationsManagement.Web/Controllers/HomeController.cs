using AutoMapper;
using CustomerRelationsManagement.Application.Contracts;
using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Common.Models;
using CustomerRelationsManagement.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CustomerRelationsManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Employee> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IProjectRepository projectRepository;
        private readonly IProjectTaskRepository projectTaskRepository;
        private readonly IAnnouncementRepository announcementRepository;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context, IMapper mapper,
            UserManager<Employee> userManager,
            IHttpContextAccessor httpContextAccessor,
            IProjectRepository projectRepository,
            IProjectTaskRepository projectTaskRepository,
            IAnnouncementRepository announcementRepository)
        {
            _logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.projectRepository = projectRepository;
            this.projectTaskRepository = projectTaskRepository;
            this.announcementRepository = announcementRepository;
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var announcements = mapper.Map<List<AnnouncementViewModel>>(await announcementRepository.GetRecentAnnouncements());
            
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var projects = await projectRepository.GetRangeAsync(user.Id);

            var contents = new ProjectsAndAnnouncementsViewModel()
            {
                Projects = projects,
                Announcements = announcements,
            };
            return View(contents);
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult CreateTask(int projectId)
        {
            return RedirectToAction("Create", "ProjectTasks", new { projectId = projectId});
        }

        [Authorize]
        public async Task<IActionResult> ProjectDetails(int projectId)
        {
            try
            {
                var project = await projectRepository.GetProjectAsync(projectId);

                if (project == null)
                {
                    return NotFound("Project task not found.");
                }

                var projectTasks = mapper.Map<List<ProjectTaskViewModel>>(await projectTaskRepository.GetRangeAsync(project.Id));

                var model = new ProjectProjectTasksViewModel
                {
                    Project = project,
                    ProjectTasks = projectTasks
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while viewing details for the project.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while viewing details for the project.");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}