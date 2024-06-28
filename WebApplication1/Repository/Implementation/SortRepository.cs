using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository.Abstractions;

namespace WebApplication1.Repository.Implementation
{
    public class SortRepository : ISortRepository
    {
        private readonly DataContext _context;

        public SortRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            if(categories.Count < 0)
                return null;

            return categories;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            var cities = await _context.Cities.ToListAsync();
            if (cities.Count < 0)
                return null;

            return cities;
        }

        public async Task<IEnumerable<Company>> GetCompanyAsync()
        {
            var companies = await _context.Companies.ToListAsync();
            if (companies.Count < 0)
                return null;

            return companies;
        }
    }
}
