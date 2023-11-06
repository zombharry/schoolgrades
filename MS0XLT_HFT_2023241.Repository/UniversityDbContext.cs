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


        public UniversityDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UniversityDb.mdf;Integrated Security=True;MultipleActiveResultSets=true";

                optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("University");
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

            modelBuilder.Entity<Subject>().HasData(new Subject[]
            {
                new Subject{SubjectId=1,SubjectName="Database",Credit=4 },
                new Subject{SubjectId=2,SubjectName="Computer Architectures",Credit=5 },
                new Subject{SubjectId=3,SubjectName="Programming 1",Credit=6 },
                new Subject{SubjectId=4,SubjectName="Calculus 1",Credit=6 },
                new Subject{SubjectId=5,SubjectName="Programming 2",Credit=4 },
                new Subject{SubjectId=6,SubjectName="HTML",Credit=3 }
            });
            modelBuilder.Entity<Student>().HasData(new Student[]
            {
                new Student{StudenttId=1,StudentName="John Doe",Semester=3},
                new Student{StudenttId=2,StudentName="Jane Doe",Semester=1},
                new Student{StudenttId=3,StudentName="John Smith",Semester=2},
                new Student{StudenttId=4,StudentName="Harry Potter",Semester=5},
                new Student{StudenttId=5,StudentName="Peter Grifin",Semester=6},
                new Student{StudenttId=6,StudentName="Jack Daniel",Semester=4},
                new Student{StudenttId=7,StudentName="Ozzy Osbourne",Semester=3}
            });
            modelBuilder.Entity<Grade>().HasData(new Grade[] {
                new Grade{GradeId=1, SubjectId=1, StudentId=1, GradeValue=4,Date=new DateTime(2020,11,25) },
                new Grade{GradeId=2, SubjectId=2, StudentId=1, GradeValue=2,Date=new DateTime(2020,11,30) },
                new Grade{GradeId=3, SubjectId=6, StudentId=1, GradeValue=5,Date=new DateTime(2020,12,8) },
                new Grade{GradeId=4, SubjectId=1, StudentId=2, GradeValue=4,Date=new DateTime(2020,9,25) },
                new Grade{GradeId=5, SubjectId=3, StudentId=2, GradeValue=2,Date=new DateTime(2020,11,25) },
                new Grade{GradeId=6, SubjectId=4, StudentId=2, GradeValue=1,Date=new DateTime(2020,12,11) },
                new Grade{GradeId=7, SubjectId=2, StudentId=3, GradeValue=5,Date=new DateTime(2021,1,25) },
                new Grade{GradeId=8, SubjectId=5, StudentId=3, GradeValue=4,Date=new DateTime(2020,11,25) },
                new Grade{GradeId=9, SubjectId=6, StudentId=3, GradeValue=2,Date=new DateTime(2020,11,17) },
                new Grade{GradeId=10, SubjectId=3, StudentId=4, GradeValue=5,Date=new DateTime(2020,11,25) },
                new Grade{GradeId=11, SubjectId=4, StudentId=4, GradeValue=4,Date=new DateTime(2020,11,30) },
                new Grade{GradeId=12, SubjectId=6, StudentId=4, GradeValue=3,Date=new DateTime(2020,11,25) },
                new Grade{GradeId=13, SubjectId=1, StudentId=5, GradeValue=2,Date=new DateTime(2022,4,4) },
                new Grade{GradeId=14, SubjectId=4, StudentId=5, GradeValue=1,Date=new DateTime(2020,11,25) },
                new Grade{GradeId=15, SubjectId=5, StudentId=5, GradeValue=5,Date=new DateTime(2021,3,13) },
                new Grade{GradeId=16, SubjectId=2, StudentId=6, GradeValue=2,Date=new DateTime(2022,11,25) },
                new Grade{GradeId=17, SubjectId=3, StudentId=6, GradeValue=3,Date=new DateTime(2021,6,7) },
                new Grade{GradeId=18, SubjectId=4, StudentId=6, GradeValue=5,Date=new DateTime(2019,12,5) },
                new Grade{GradeId=19, SubjectId=1, StudentId=7, GradeValue=4,Date=new DateTime(2021,10,19) },
                new Grade{GradeId=20, SubjectId=6, StudentId=7, GradeValue=5,Date=new DateTime(2022,1,25) },
            });
        }

    }
}
