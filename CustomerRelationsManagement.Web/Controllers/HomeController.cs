using AutoMapper;
using CustomerRelationsManagement.Web.Contracts;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;
using CustomerRelationsManagement.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper, UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor, IProjectRepository projectRepository)
        {
            _logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.projectRepository = projectRepository;
        }

        public async Task<IActionResult> Index()
        {
            var announcements = mapper.Map<List<AnnouncementViewModel>>(context.Announcements.OrderByDescending(a => a.DatePublished).Take(3).ToList());
            
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var projects = await projectRepository.GetRangeAsync(user.Id);

            var contents = new ProjectsAndAnnouncementsViewModel()
            {
                Projects = projects,
                Announcements = announcements
            };
            return View(contents);
        }

        public async Task<IActionResult> ProjectDetails(int projectId)
        {
            var model = mapper.Map<ProjectViewModel>(await projectRepository.GetAsync(projectId));

            if(model == null)
            {
                return NotFound();
            }
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