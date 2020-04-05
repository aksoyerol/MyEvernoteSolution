using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Dal;

namespace MyEvernote.Bll.Repository
{
    public class RepositoryBase
    {
        protected static DatabaseContext db;
        private static object _lockSync = new object();

        public RepositoryBase()
        {
            db = CreateContext();
        }

        public static DatabaseContext CreateContext()
        {
            if (db == null)
            {
                lock (_lockSync)
                {
                    if (db == null)
                    {
                        db = new DatabaseContext();
                    }
                }

            }
            return db;
        }
    }
}
