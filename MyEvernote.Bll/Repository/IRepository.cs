using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Bll.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> List();
        IQueryable<T> List(Expression<Func<T,bool>> filter);
        T Find(Expression<Func<T, bool>> filter);
        int Insert(T entity);
        int Update(T entity);
        int Delete(T entity);
        int Save();
        
    }
}
