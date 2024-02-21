using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GnosisNet.Entities.Entities.Identity;
using GnosisNet.Entities.Entities;
using Microsoft.AspNetCore.Identity;

namespace GnosisNet.Data
{
    public class GnosisDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public GnosisDbContext(DbContextOptions<GnosisDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.CreatedBy); // Assuming CreatedBy is the foreign key column name

            base.OnModelCreating(modelBuilder);
        }
        public virtual async Task<int> SaveChangesAsync(string userId = null!)
        {
            DateTime now = DateTime.Now;
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry changedEntity in ChangeTracker.Entries())
            {

                if (changedEntity.Entity is BaseEntity entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedOn = DateTime.Now.ToUniversalTime();
                            entity.UpdatedOn = DateTime.Now.ToUniversalTime();
                            entity.CreatedBy = userId;
                            entity.IsActive = true;
                            break;

                        case EntityState.Modified:
                            Entry(entity).Property(x => x.UpdatedOn).IsModified = false;
                            entity.UpdatedOn = DateTime.Now.ToUniversalTime();
                            entity.UpdatedBy = userId;
                            break;
                    }
                }
            }
            var result = await base.SaveChangesAsync();
            return result;
        }
    }
}
