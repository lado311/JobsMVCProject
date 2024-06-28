using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;
using WebApplication1.Repository.Abstractions;
using WebApplication1.Repository.Implementation;

namespace WebApplication1.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _repository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;
        public CompanyController(IVacancyRepository vacancyRepository, ICompanyRepository repository, ITokenRepository tokenRepository, IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _tokenRepository = tokenRepository;
            _configuration = configuration;
            _accessor = accessor;
            _vacancyRepository = vacancyRepository;

        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> AddCompanyView()
        {
            return View("CompanyAddView");
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyView(Company company)
        {
            if(company == null)
                return View("CompanyAddView");
            else
            {
                company.Token = _tokenRepository.GenerateToken(company, _configuration);


                //if user registration has been successfully then return true, else return false 
                var registerResultIsSuccessfull = await _repository.AddCompany(company);
                if (!registerResultIsSuccessfull)
                    return BadRequest();


                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult LogInCompanyView()
        {
            return View("LogInCompanyView");
        }

        [HttpPost]
        public async Task<IActionResult> LogInCompany(Company company)
        {
            if (company == null)
                return BadRequest();

            Company currentCompany = await _repository.LogInCompany(company.Email, company.Password);

            if(currentCompany == null)
            {
                return View("LogInCompanyView");
            }
            _accessor.HttpContext.Session.Clear();
            _accessor.HttpContext.Session.SetInt32("companyId", currentCompany.Id);
            _accessor.HttpContext.Session.SetString("companyName", currentCompany.Name);
            _accessor.HttpContext.Session.SetString("companyEmail", currentCompany.Email);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyVacancies()
        {
            int id = (int)_accessor.HttpContext.Session.GetInt32("companyId");
            return View("CompanyVacanciesView", await _vacancyRepository.GetCompanyVacancies(id));
        }

        [HttpGet]
        public async Task<IActionResult> AddVacancy()
        {
            return View("AddVacancyView");
        }

        [HttpPost]
        public async Task<IActionResult> AddVacancy(Vacancy vacancy, Category category, City city)
        {
            int companyId = (int)_accessor.HttpContext.Session.GetInt32("companyId");
            vacancy.Author = _accessor.HttpContext.Session.GetString("companyName");
            vacancy.CompanyId = companyId;
            vacancy.StartDate = DateTime.Now;

            if (vacancy.Salary > 0)
                vacancy.ExistSalary = true;
            else
                vacancy.ExistSalary = false;

            await _repository.AddVacancy(vacancy);
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> GetVacancyResumes(Vacancy vacancy)
        {
            return View("VacancyResumeView", await _repository.GetResumes(vacancy));
        }



    }
}
