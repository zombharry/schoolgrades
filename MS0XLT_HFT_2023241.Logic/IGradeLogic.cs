using MS0XLT_HFT_2023241.Models;
using System.Collections.Generic;

namespace MS0XLT_HFT_2023241.Logic
{
    public interface IGradeLogic
    {
        void Create(Grade item);
        void Delete(int id);
        Grade Read(int id);
        IEnumerable<Grade> ReadAll();
        void Update(Grade item);
    }
}