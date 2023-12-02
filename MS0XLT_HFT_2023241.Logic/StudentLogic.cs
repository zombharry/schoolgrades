using System;
using System.Collections;
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
        public IEnumerable<StudentInfo> AllAvarageGrade()
        {
            var students = this.repo.ReadAll();
            return students.Select(result => new StudentInfo
            {
                StudentId = result.StudentId,
                GradeAvg = result.Grades.Average(g => g.GradeValue)
            }).ToList();
        }

        public double GetAvarageGrade(int studentId)
        {
            return this.Read(studentId).Grades.Average(g => g.GradeValue);

        }

        public IEnumerable<Student> FailedStudents()
        {
            return this.repo.ReadAll().Where(s => s.Grades.Any(g => g.GradeValue == 1)).ToList();
        }
        public IEnumerable<StudentInfo> StudentsCredits()
        {
            return this.repo.ReadAll().Select(s => new StudentInfo
            {
                StudentId = s.StudentId,
                NumberOfCredits = s.Grades.Where(g => g.GradeValue > 1).Select(c => c.Subject.Credit).Sum() 
            }).ToList();
        }
        public class StudentInfo
        {
            public int StudentId { get; set; }
            public int NumberOfCredits { get; set; }
            public double GradeAvg { get; set; }

            public override bool Equals(object obj)
            {
                StudentInfo b = obj as StudentInfo;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.StudentId == b.StudentId
                        && this.NumberOfCredits == b.NumberOfCredits
                        && this.GradeAvg == b.GradeAvg;
                }


            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.StudentId, this.GradeAvg, this.NumberOfCredits);
            }

            public class SimpleStudentComparer : IComparer
            {
                public int Compare(object x, object y)
                {
                    Student studentX = x as Student;
                    Student studentY = y as Student;

                    return studentX.StudentId.CompareTo(studentY.StudentId);
                }
            }
        }

    }
}
