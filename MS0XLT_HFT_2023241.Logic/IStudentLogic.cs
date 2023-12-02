using MS0XLT_HFT_2023241.Models;
using System.Collections.Generic;
using static MS0XLT_HFT_2023241.Logic.StudentLogic;

namespace MS0XLT_HFT_2023241.Logic
{
    public interface IStudentLogic
    {
        IEnumerable<StudentInfo> AllAvarageGrade();
        void Create(Student item);
        void Delete(int id);
        IEnumerable<Student> FailedStudents();
        double GetAvarageGrade(int studentId);
        Student Read(int id);
        IEnumerable<Student> ReadAll();
        IEnumerable<StudentInfo> StudentsCredits();
        void Update(Student item);
    }
}