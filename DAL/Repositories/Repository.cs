using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SJMDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(SJMDbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public bool Add(T entity)
        {
            this._dbSet.Add(entity);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            this._dbSet.Remove(entity);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public T Get(int id)
        {
            return this._dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return this._dbSet.ToList();
        }

        public bool Update(T entity)
        {
            this._dbSet.Update(entity);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
