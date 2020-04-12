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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //FLUENT API

            modelBuilder.Entity<Note>()
                .HasMany(n => n.Comment)
                .WithRequired(c => c.Note)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Note>()
                .HasMany(n=>n.Liked)
                .WithRequired(c=>c.Note)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Category>().HasMany(n=>n.Note).WithRequired(c=>c.Category)
                .WillCascadeOnDelete(true);
            
        }
    }


}
