using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities;

namespace MyEvernote.Dal
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext() : base("MyEvernoteDb")
        {
                
        }

        public DbSet<EvernoteUser> EvernoteUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Liked> Liked { get; set; }
        public DbSet<Note> Note { get; set; }

        
    }
}
