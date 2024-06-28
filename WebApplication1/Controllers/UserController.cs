using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Dto;
using WebApplication1.Repository.Abstractions;
using WebApplication1.Repository.Implementation;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {

        private readonly IVacancyRepository _vacancyRepository;
        private readonly IHttpContextAccessor _accessor;

        public UserController(IVacancyRepository vacancyRepository, IHttpContextAccessor accessor)
        {
            _vacancyRepository = vacancyRepository;
            _accessor = accessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FullVacancyView()
        {
            _accessor.HttpContext.Session.Clear();
            var vacancies = await _vacancyRepository.GetVacancies();
            return View("FullVacancyView", vacancies);
        }

        [HttpGet]
        public async Task<IActionResult> GetVacanciesByCategory(Category category)
        {
            var vacancies = await _vacancyRepository.GetVacanciesByCategory(category.Id);

            return View("VacanciesByCategoryView", vacancies);
        }

        [HttpGet]
        public async Task<IActionResult> GetVacanciesByCity(int id)
        {
            var vacancies = await _vacancyRepository.GetVacanciesByCity(id);

            return View("VacanciesByCityView", vacancies);
        }

        [HttpGet]
        public async Task<IActionResult> GetVacanciesByCompany(int id)
        {
            var vacancies = await _vacancyRepository.GetVacanciesByCompany(id);

            return View("VacanciesByCompanyView", vacancies);
        }


        [HttpGet]
        public async Task<IActionResult> SendResume()
        {
            if (_accessor.HttpContext.Session.GetString("companyName") != null)
                return RedirectToAction("Index", "Home");

            return View("SendResumeView");
        }

        [HttpPost]
        public async Task<IActionResult> SendResume(Vacancy vacancy, ResumeDetail resumeDto)
        {
            vacancy.CompanyId = (int)_accessor.HttpContext.Session.GetInt32("companyId");
             _vacancyRepository.SendResume(vacancy, resumeDto);

            return RedirectToAction("Index","Home");
        }

    }
}
