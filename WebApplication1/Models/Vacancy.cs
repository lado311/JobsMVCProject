namespace WebApplication1.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadLine { get; set; }
        public int CompanyId { get; set; }
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public decimal Salary { get; set; }
        public bool ExistSalary { get; set; }



    }
}
