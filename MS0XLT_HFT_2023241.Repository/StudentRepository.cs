using MS0XLT_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS0XLT_HFT_2023241.Repository
{
    public class StudentRepository : Repository<Student>, IRepository<Student>
    {
        public StudentRepository(UniversityDbContext context) : base(context)
        {
        }
        public override Student Read(int id)
        {
            return this.context.Students.First(t => t.StudentId == id);
        }

        public override void Update(Student item)
        {
            var old = Read(item.StudentId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
        }
    }
}
