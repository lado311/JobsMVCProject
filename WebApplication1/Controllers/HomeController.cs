using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Repository.Abstractions;
using WebApplication1.Repository.Implementation;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly IVacancyRepository _vacancyRepository;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor, IVacancyRepository repository)
        {
            _logger = logger;
            _accessor = accessor;
            _vacancyRepository = repository;
        }

        public IActionResult Index()
        {
            var vacancies = _vacancyRepository.GetVacancies().Result;
            return View(vacancies);
        }

        public IActionResult Privacy()
        {
            _accessor.HttpContext.Session.Clear();
            return View();
        }

        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}