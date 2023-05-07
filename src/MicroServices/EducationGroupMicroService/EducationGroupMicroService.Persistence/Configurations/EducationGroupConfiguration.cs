using EducationGroupMicroService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationGroupMicroService.Persistence.Configurations
{
    public class EducationGroupConfiguration : IEntityTypeConfiguration<EducationGroup>
    {
        public void Configure(EntityTypeBuilder<EducationGroup> builder)
        {
            builder.ToTable("educationgroup");
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).HasColumnName("id");

            builder.Property(k => k.Title).HasColumnName("title").IsRequired(false).HasMaxLength(250);
            builder.Property(k => k.StartDate).HasColumnName("start_date").HasColumnType("timestamp").IsRequired(true);
            builder.Property(k => k.EndDate).HasColumnName("end_date").HasColumnType("timestamp").IsRequired(true);
            builder.Property(k => k.CreatedDate).HasColumnName("created_date").HasColumnType("timestamp").IsRequired(true);
            builder.Property(k => k.ModifiedDate).HasColumnName("modified_date").HasColumnType("timestamp").IsRequired(true);
            builder.Property(k => k.Status).HasColumnName("status").IsRequired(true);
            builder.Property(k => k.IsDeleted).HasColumnName("is_deleted").IsRequired(true);
        }
    }
}
