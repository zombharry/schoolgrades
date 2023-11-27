using MS0XLT_HFT_2023241.Models;
using System.Collections.Generic;

namespace MS0XLT_HFT_2023241.Logic
{
    public interface IStudentLogic
    {
        IEnumerable<object> AllAvarageGrade();
        void Create(Student item);
        void Delete(int id);
        double GetAvarageGrade(int studentId);
        Student Read(int id);
        IEnumerable<Student> ReadAll();
        void Update(Student item);
    }
}