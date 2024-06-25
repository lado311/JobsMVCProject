using WebApplication1.Models;

namespace WebApplication1.Repository.Abstractions
{
    public interface IVacancyRepository
    {
        Task<IEnumerable<Vacancy>> GetVacancies();
        Task<IEnumerable<Vacancy>> GetCompanyVacancies(int id);
    }
}
