using AutoMapper;
using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;
using CustomerRelationsManagement.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CustomerRelationsManagement.Web.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Index()
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

        public ActionResult CreateTask(int projectId)
        {
            return RedirectToAction("Create", "ProjectTasks", new { projectId = projectId});
        }

        public async Task<IActionResult> ProjectDetails(int projectId)
        {
            try
            {
                var project = await projectRepository.GetAsync(projectId);

                if (project == null)
                {
                    return NotFound("Project task not found.");
                }

                var projectTasks = mapper.Map<List<ProjectTaskViewModel>>(await projectTaskRepository.GetRangeAsync(project.Id));

                var model = new ProjectProjectTasksViewModel
                {
                    Project = mapper.Map<ProjectViewModel>(project),
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