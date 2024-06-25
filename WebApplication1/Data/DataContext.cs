using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DataContext : DbContext
    {

        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }

        private readonly IConfiguration _configuration;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public bool CanConnectToDatabase()
        {
            try
            {
                return Database.CanConnect();
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Database connection check failed: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return false;
            }
        }
    }
}
