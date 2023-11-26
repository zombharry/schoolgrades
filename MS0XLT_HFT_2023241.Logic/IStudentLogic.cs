using MS0XLT_HFT_2023241.Models;
using System.Linq;

namespace MS0XLT_HFT_2023241.Logic
{
    public interface IStudentLogic
    {
        IQueryable AllAvarageGrade();
        void Create(Student item);
        void Delete(int id);
        double GetAvarageGrade(int studentId);
        Student Read(int id);
        IQueryable<Student> ReadAll();
        void Update(Student item);
    }
}