using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository.Abstractions;

namespace WebApplication1.Repository.Implementation
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;


        public CompanyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCompany(Company company)
        {
            string password = CreatePasswordHash(company.Password, out byte[] passwordHash, out byte[] passwordSalt);
            if (company == null)
                return false;

            company.Password = password;
            company.PasswordHash = passwordHash;
            company.PasswordSalt = passwordSalt;
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            //if user registration has been successfully then return true, else return false 
            return true;
        }

        public async Task AddVacancy(Vacancy vacancy)
        {
            await _context.Vacancies.AddAsync(vacancy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVacancy(Vacancy vacancy)
        {
            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResumeDetail>> GetResumes(Vacancy vacancy)
        {
            var resumes = _context.Resumes.Include(r => r.ResumeDetail).ToList().Where(r => r.VacancyId == vacancy.Id);

            if (resumes.Count() < 0)
                return null;

            var resumeDetails = resumes.Select(r => r.ResumeDetail).ToList();
            return resumeDetails.ToList();
        }

        public async Task<Company> LogInCompany(string email, string password)
        {
            var authorizedCompany = await _context.Companies.FirstOrDefaultAsync(c => c.Password == password && c.Email == email);

            if (authorizedCompany == null)
                return null;
                
            return authorizedCompany;

        }



        private string CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            };

            return password;
        }
    }
}
