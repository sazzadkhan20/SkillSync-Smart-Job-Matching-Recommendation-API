using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public T Get(int id);
        public List<T> GetAll();
        public bool Add(T entity);
        public bool Delete(T entity);
        public bool Update(T entity);
    }
}
