using MS0XLT_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS0XLT_HFT_2023241.Repository
{
    public class GradeRepository : Repository<Grade>, IRepository<Grade>
    {
        public GradeRepository(UniversityDbContext context) : base(context)
        {
        }

        public override Grade Read(int id)
        {
            return this.context.Grades.First(t => t.GradeId == id);
        }

        public override void Update(Grade item)
        {
            var old = Read(item.GradeId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            this.context.SaveChanges();
        }
    }
}
