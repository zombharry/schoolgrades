using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MS0XLT_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected UniversityDbContext context;
        public Repository(UniversityDbContext context)
        {
                this.context = context;
        }
        public void Create(T item)
        {
            context.Set<T>().Add(item);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Set<T>().Remove(Read(id));
            context.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return context.Set<T>();
        }

        public abstract T Read(int id);

        public abstract void Update(T item);
    }
}
