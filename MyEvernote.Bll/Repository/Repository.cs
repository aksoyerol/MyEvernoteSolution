using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Dal;

namespace MyEvernote.Bll.Repository
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {

        private DbSet<T> _context;

        public Repository()
        {
            _context = db.Set<T>();
        }

        public T Find(Expression<Func<T, bool>> filter)
        {
            return _context.FirstOrDefault(filter);
        }

        public List<T> List()
        {
            return _context.ToList();
        }

        public IQueryable<T> List(Expression<Func<T, bool>> filter)
        {
            return _context.Where(filter);
        }

        public IQueryable<T> ListQueryable()
        {
            return _context.AsQueryable();
        }

        public int Insert(T entity)
        {
            _context.Add(entity);
            return Save();
        }


        public int Update(T entity)
        {
            return Save();
        }


        public int Delete(T entity)
        {
            _context.Remove(entity);
            return Save();
        }


        public int Save()
        {
            return db.SaveChanges();
        }

    }
}
