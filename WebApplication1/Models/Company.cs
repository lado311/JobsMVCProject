namespace WebApplication1.Models
{
    public class Company 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyImage { get; set; }
        public List<Vacancy> Vacancies { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpire { get; set; }


    }
}
