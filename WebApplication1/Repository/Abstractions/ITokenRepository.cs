using WebApplication1.Models;

namespace WebApplication1.Repository.Abstractions
{
    public interface ITokenRepository
    {
        string GenerateToken(Company company, IConfiguration configuration);
    }
}
