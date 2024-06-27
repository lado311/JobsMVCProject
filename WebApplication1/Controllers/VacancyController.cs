using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository.Abstractions;

namespace WebApplication1.Controllers
{
    public class VacancyController : Controller
    {

        private readonly IVacancyRepository _vacancyRepositroy;

        public VacancyController(IVacancyRepository vacancyRepository)
        {
            _vacancyRepositroy = vacancyRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> FullVacancyView()
        {
            var vacancies = await _vacancyRepositroy.GetVacancies();
            return View("User/FullVacancyView", vacancies);
        }

        [HttpGet]   
        public async Task<IActionResult> GetVacanciesByCategory(int id)
        {
            var vacancies = await _vacancyRepositroy.GetVacanciesByCategory(id);

            return View("VacanciesByCategoryView", vacancies);
        }

        [HttpGet]
        public async Task<IActionResult> GetVacanciesByCity(int id)
        {
            var vacancies = await _vacancyRepositroy.GetVacanciesByCity(id);

            return View("VacanciesByCityView", vacancies);
        }

        [HttpGet]
        public async Task<IActionResult> GetVacanciesByCompany(int id)
        {
            var vacancies = await _vacancyRepositroy.GetVacanciesByCompany(id);

            return View("VacanciesByCompanyView", vacancies);
        }
    }
}
