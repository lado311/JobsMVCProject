using WebApplication1.Models;

namespace WebApplication1.Repository.Abstractions
{
    public interface ICompanyRepository
    {
        Task<bool> AddCompany(Company company);
        Task<Company> LogInCompany(string email, string password);
        Task DeleteVacancy(Vacancy vacancy);
        
    }
}
