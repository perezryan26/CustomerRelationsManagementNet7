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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Employee> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IProjectRepository projectRepository;
        private readonly IProjectTaskRepository projectTaskRepository;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper, UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor, IProjectRepository projectRepository, IProjectTaskRepository projectTaskRepository)
        {
            _logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.projectRepository = projectRepository;
            this.projectTaskRepository = projectTaskRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var announcements = mapper.Map<List<AnnouncementViewModel>>(context.Announcements.OrderByDescending(a => a.DatePublished).Take(3).ToList());
            
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
            // Your logic here

            // Redirect to the OtherAction action in OtherController
            return RedirectToAction("Create", "ProjectTasks", new { projectId = projectId});
        }

        public async Task<IActionResult> ProjectDetails(int projectId)
        {

            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            

            var project = await projectRepository.GetAsync(projectId);

            if (project == null)
            {
                return NotFound();
            }

            var projectTasks = mapper.Map<List<ProjectTaskViewModel>>(await context.ProjectTasks.Where(q => q.ProjectId == project.Id).ToListAsync());

            var model = new ProjectProjectTasksViewModel
            {
                Project = mapper.Map<ProjectViewModel>(project),
                ProjectTasks = projectTasks
            };

            
            return View(model);
        }

        [Authorize]
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