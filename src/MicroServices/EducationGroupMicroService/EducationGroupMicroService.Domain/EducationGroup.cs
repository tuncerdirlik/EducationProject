using EducationGroupMicroService.Domain.Common;

namespace EducationGroupMicroService.Domain
{
    public class EducationGroup : BaseDomainEntity
    {
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}
