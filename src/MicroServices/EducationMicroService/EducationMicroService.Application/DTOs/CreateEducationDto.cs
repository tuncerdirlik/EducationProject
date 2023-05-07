namespace EducationMicroService.Application.DTOs
{
    public class CreateEducationDto
    {
        public int EducationGroupId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
