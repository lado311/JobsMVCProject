using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository.Abstractions;

namespace WebApplication1.Repository.Implementation
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly DataContext _context;
        public VacancyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vacancy>> GetCompanyVacancies(int id)
        {
            var company = await _context.Companies.Include(c => c.Vacancies).FirstOrDefaultAsync(c => c.Id == id);

            return company.Vacancies;
        }

        public async Task<IEnumerable<Vacancy>> GetVacancies()
        {
            var company = _context.Companies.Include(c => c.Vacancies);
            List<Vacancy> vacancies = new List<Vacancy>();
            foreach (var item in company)
            {
                item.Vacancies.ForEach(v => v.Author = item.Name);
                vacancies.AddRange(item.Vacancies);
            }

            return vacancies;
        }

        public async Task<IEnumerable<Vacancy>> GetVacanciesByCategory(int categoryId)
        {
            var category = await _context.Categories.Include(c => c.Vacancies).FirstOrDefaultAsync(c => c.Id == categoryId);
            
            if(category == null)
                return null;

            return category.Vacancies;

        }

        public async Task<IEnumerable<Vacancy>> GetVacanciesByCity(int cityId)
        {
            var city = await _context.Cities.Include(c => c.Vacancies).FirstOrDefaultAsync(c => c.Id == cityId);
            if (city == null)
                return null;

            return city.Vacancies;
        }

        public async Task<IEnumerable<Vacancy>> GetVacanciesByCompany(int companyId)
        {
            var company = await _context.Companies.Include(c => c.Vacancies).FirstOrDefaultAsync(c => c.Id == companyId);
            if (company == null)
                return null;

            return company.Vacancies;
        }
    }
}
