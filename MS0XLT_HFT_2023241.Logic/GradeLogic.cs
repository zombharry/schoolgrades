using MS0XLT_HFT_2023241.Models;
using MS0XLT_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS0XLT_HFT_2023241.Logic
{
    public class GradeLogic : IGradeLogic
    {
        IRepository<Grade> repo;

        public GradeLogic(IRepository<Grade> repo)
        {
            this.repo = repo;
        }
        public void Create(Grade item)
        {
            if (item.GradeValue < 1 || item.GradeValue > 5)
            {
                throw new ArgumentException("Name is not valid");
            }
            this.repo.Create(item);
        }
        public IEnumerable<Grade> ReadAll()
        {
            return this.repo.ReadAll().ToList();
        }
        public Grade Read(int id)
        {
            return this.repo.Read(id);
        }
        public void Update(Grade item)
        {
            this.repo.Update(item);
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }
    }
}
