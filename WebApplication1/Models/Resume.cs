namespace WebApplication1.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int VacancyId { get; set; }
        public int ResumeDetailId { get; set; }
        public ResumeDetail ResumeDetail { get; set; }

    }
}
