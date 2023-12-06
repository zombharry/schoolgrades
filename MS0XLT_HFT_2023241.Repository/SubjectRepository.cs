using MS0XLT_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS0XLT_HFT_2023241.Repository
{
    public class SubjectRepository : Repository<Subject>, IRepository<Subject>
    {
        public SubjectRepository(UniversityDbContext context) : base(context)
        {
        }
        public override Subject Read(int id)
        {
            return this.context.Subjects.First(t => t.SubjectId == id);
        }

        public override void Update(Subject item)
        {
            var old = Read(item.SubjectId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
        }
    }
}
