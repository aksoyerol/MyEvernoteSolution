using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Bll.Repository;
using MyEvernote.Entities;

namespace MyEvernote.Bll
{
    public class CategoryManager : IRepository<Category>
    { 
        private Repository<Category> repo_category = new Repository<Category>();

        public int Delete(Category entity)
        {
            return repo_category.Delete(entity);
        }

        public IQueryable<Category> List(Expression<Func<Category, bool>> filter)
        {
            return repo_category.List(filter);
        }

        public Category Find(Expression<Func<Category, bool>> filter)
        {
            return repo_category.Find(filter);
        }

        public Category GetCategoryId(int id)
        {
            return repo_category.Find(x => x.Id == id);
        }

        public int Insert(Category entity)
        {
            return repo_category.Insert(entity);
        }

        public List<Category> List()
        {
            return repo_category.List();
        }

        public IQueryable<Category> ListQueryable(Expression<Func<Category, bool>> filter)
        {
            return repo_category.List(filter);
        }


        public int Save()
        {
            return repo_category.Save();
        }

        public int Update(Category entity)
        {
            return repo_category.Update(entity);
        }
    }
}
