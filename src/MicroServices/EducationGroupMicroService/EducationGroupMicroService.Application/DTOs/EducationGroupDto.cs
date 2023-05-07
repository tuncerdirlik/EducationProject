namespace EducationGroupMicroService.Application.DTOs
{
    public class EducationGroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}
