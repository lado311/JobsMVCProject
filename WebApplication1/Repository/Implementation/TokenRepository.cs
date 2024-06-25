using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Repository.Abstractions;

namespace WebApplication1.Repository.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        //Create Token Method
        public string GenerateToken(Company company, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, company.Id.ToString()),
                new Claim(ClaimTypes.Name, company.Name),
                new Claim(ClaimTypes.Email, company.Email),
            };

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
