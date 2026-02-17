using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Auth.Core.Entities;
using Auth.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasIndex(x => x.UserName).IsUnique();

                entity.Property(x => x.UserName).IsRequired().HasMaxLength(100);
                entity.Property(x => x.PasswordHash).IsRequired();

                entity.Property(x => x.Role).IsRequired();

                entity.Property(x => x.CreatedAt);
                entity.Property(x => x.LastLogin);
            });

            builder.Entity<AppCategory>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Name).IsUnique();
            });

            builder.Entity<AppProduct>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(x => x.Price)
                      .HasPrecision(18, 2);

                entity.Property(x => x.UnitsInStock);

                entity.Property(x => x.CreatedAt);
                entity.Property(x => x.UpdatedAt);
            });
        }
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<AppProduct> Products => Set<AppProduct>();
        public DbSet<AppCategory> Categories => Set<AppCategory>();
    }
}
