using System;
using System.Collections.Generic;
using System.Linq;
using MS0XLT_HFT_2023241.Models;
using MS0XLT_HFT_2023241.Repository;

namespace MS0XLT_HFT_2023241.Logic
{
    public class StudentLogic : IStudentLogic
    {
        IRepository<Student> repo;
        public StudentLogic(IRepository<Student> repo)
        {
            this.repo = repo;
        }
        public void Create(Student item)
        {
            if (item.StudentName.Length < 2 || item.StudentName.Length > 25)
            {
                throw new ArgumentException("Name is not valid");
            }
            this.repo.Create(item);
        }
        public IEnumerable<Student> ReadAll()
        {
            return this.repo.ReadAll().ToList();
        }
        public Student Read(int id)
        {
            return this.repo.Read(id);
        }
        public void Update(Student item)
        {
            this.repo.Update(item);
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }
        public IEnumerable<object> AllAvarageGrade()
        {
            var students = this.repo.ReadAll();
            return students.Select(result => new { StudentId = result.StudentId, AvarageGrade = result.Grades.Average(g => g.GradeValue) }).ToList();
        }

        public double GetAvarageGrade(int studentId)
        {
            return this.Read(studentId).Grades.Average(g => g.GradeValue);

        }

        public IEnumerable<Student> FailedCount()
        {
           return this.repo.ReadAll().Where(s => s.Grades.Any(g => g.GradeValue==1)).ToList();
        }
        public IEnumerable<object> StudentsCredits()
        {
            return this.repo.ReadAll().Select(s => new 
            {
                StudentId = s.StudentId,
                NumberOfCredits = s.Grades.Any(g => g.GradeValue != 1) ? s.Grades.Sum(c => c.Subject.Credit):0 
            }).ToList();
        }
    }
}
