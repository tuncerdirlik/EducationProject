using EducationGroupMicroService.Domain;
using EducationGroupMicroService.Domain.Common;
using EducationGroupMicroService.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EducationGroupMicroService.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<EducationGroup> EducationGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EducationGroupConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.ModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.IsDeleted = false;
                    entry.Entity.CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
