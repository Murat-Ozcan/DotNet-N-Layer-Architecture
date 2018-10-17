using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MRTFramework.DataAccessLayer.EntityFrameworkCore.EntityTypeConfigurations;
using MRTFramework.Model.BaseModels.Concrete;

namespace MRTFramework.DataAccessLayer.EntityFrameworkCore.Context
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LogEntityTypeConfiguration());

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>().HasOne(u => u.User).WithMany(ur => ur.UserRoles)
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<UserRole>().HasOne(r => r.Role).WithMany(ur => ur.UserRoles)
                .HasForeignKey(r => r.RoleId);
        }
    }
}
