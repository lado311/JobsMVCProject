using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Repository.Abstractions;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly IVacancyRepository _repository;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor, IVacancyRepository repository)
        {
            _logger = logger;
            _accessor = accessor;
            _repository = repository;
        }

        public IActionResult Index()
        {
            _accessor.HttpContext.Session.Clear();
            return View();

        }

        public IActionResult Privacy()
        {
            var vacancies = _repository.GetVacancies().Result;
            return View(vacancies);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}