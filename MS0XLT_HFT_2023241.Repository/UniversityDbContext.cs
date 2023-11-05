using System;
using Microsoft.EntityFrameworkCore;
using MS0XLT_HFT_2023241.Models;

namespace MS0XLT_HFT_2023241.Repository
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UniversityDb.mdf;Integrated Security=True;MultipleActiveResultSets=true";

                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(x => x.Grades)
                .WithOne()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Grade>()
                .HasOne(x=>x.Subject)
                .WithMany()
                .HasForeignKey(x=>x.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
