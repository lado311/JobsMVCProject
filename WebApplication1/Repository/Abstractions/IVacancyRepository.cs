﻿using WebApplication1.Models;
using WebApplication1.Models.Dto;

namespace WebApplication1.Repository.Abstractions
{
    public interface IVacancyRepository
    {
        Task<IEnumerable<Vacancy>> GetVacancies();
        Task<IEnumerable<Vacancy>> GetCompanyVacancies(int id);
        Task<IEnumerable<Vacancy>> GetVacanciesByCategory(int categoryId);
        Task<IEnumerable<Vacancy>> GetVacanciesByCity(int cityId);
        Task<IEnumerable<Vacancy>> GetVacanciesByCompany(int companyId);

        void SendResume(Vacancy vacancy, ResumeDetail resumeDto);

    }
}
