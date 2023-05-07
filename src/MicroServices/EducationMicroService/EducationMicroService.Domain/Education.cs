using EducationMicroService.Domain.Common;

namespace EducationMicroService.Domain
{
    public class Education : BaseDomainEntity
    {
        public int EducationGroupId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
