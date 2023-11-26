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
        public IQueryable<Student> ReadAll()
        {
            return this.repo.ReadAll();
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
        public IQueryable AllAvarageGrade()
        {
            var students = this.ReadAll();
            return students.Select(result => new { StudentId = result.StudentId, AvarageGrade = result.Grades.Average(g => g.GradeValue) });
        }

        public double GetAvarageGrade(int studentId)
        {
            return this.Read(studentId).Grades.Average(g => g.GradeValue);

        }
    }
}
