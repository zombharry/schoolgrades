using MS0XLT_HFT_2023241.Models;
using System.Collections.Generic;

namespace MS0XLT_HFT_2023241.Logic
{
    public interface ISubjectLogic
    {
        void Create(Subject item);
        void Delete(int id);
        IEnumerable<Subject> MostFailedSubjects(int number);
        Subject Read(int id);
        IEnumerable<Subject> ReadAll();
        void Update(Subject item);
    }
}