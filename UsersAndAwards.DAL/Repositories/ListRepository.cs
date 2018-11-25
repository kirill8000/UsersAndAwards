using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UsersAndAwards.DAL.Interfaces;

namespace UsersAndAwards.DAL.Repositories
{
    internal abstract class ListRepository<T>  : IRepository<T> where T : class
    {
        protected int CurId;
        protected List<T> Items = new List<T>();
        protected abstract void SetId(T v);
        public void Create(T item)
        {
            CurId++;
            Items.Add(item);
            SetId(item);
        }

        public abstract void Delete(int id);
        

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Items.Where(predicate);
        }

        public abstract T Get(int id);

        public IEnumerable<T> GetAll()
        {
            return Items;
        }

        public void Update(T item)
        {
        }
    }
}
