using EducationMicroService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationMicroService.Persistence.Configurations
{
    public class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.ToTable("education");
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).HasColumnName("id");
            
            builder.Property(k => k.EducationGroupId).HasColumnName("education_group_id").IsRequired(true);
            builder.Property(k => k.Title).HasColumnName("title").IsRequired(false).HasMaxLength(250);
            builder.Property(k => k.Description).HasColumnName("description").IsRequired(false);
            builder.Property(k => k.Link).HasColumnName("link").IsRequired(false).HasMaxLength(2000);
            builder.Property(k => k.CreatedDate).HasColumnName("created_date").HasColumnType("timestamp").IsRequired(true);
            builder.Property(k => k.ModifiedDate).HasColumnName("modified_date").HasColumnType("timestamp").IsRequired(true);
            builder.Property(k => k.IsDeleted).HasColumnName("is_deleted").IsRequired(true);
        }
    }
}
