using MS0XLT_HFT_2023241.Models;
using MS0XLT_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS0XLT_HFT_2023241.Logic
{
    public class SubjectLogic : ISubjectLogic
    {
        IRepository<Subject> repo;

        public SubjectLogic(IRepository<Subject> repo)
        {
            this.repo = repo;
        }
        public void Create(Subject item)
        {
            if (item.SubjectName.Length < 2)
            {
                throw new ArgumentException("Name is not valid");
            }
            this.repo.Create(item);
        }
        public IEnumerable<Subject> ReadAll()
        {
            return this.repo.ReadAll().ToList();
        }
        public Subject Read(int id)
        {
            return this.repo.Read(id);
        }
        public void Update(Subject item)
        {
            this.repo.Update(item);
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public IEnumerable<Subject> MostFailedSubjects(int number)
        {
            return this.repo.ReadAll().OrderByDescending(g => g.Grades.Count(v => v.GradeValue == 1)).Take(number).ToList();
        }

    }
}
