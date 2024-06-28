using WebApplication1.Models;

namespace WebApplication1.Repository.Abstractions
{
    public interface ISortRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<Company>> GetCompanyAsync();
    }
}
