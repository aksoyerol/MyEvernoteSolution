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
    public class NoteManager : IRepository<Note>
    {
        private Repository<Note> repo_note = new Repository<Note>();

        public List<Note> GetAllNotes()
        {
            return repo_note.List();
        }

        public IQueryable<Note> GetAllNotesQueryable()
        {
            return repo_note.ListQueryable();
        }

        public List<Note> ListNote()
        {
            return repo_note.List();
        }

        public List<Note> List()
        {
            return repo_note.List();
        }

        public IQueryable<Note> List(Expression<Func<Note, bool>> filter)
        {
            return repo_note.List(filter);
        }

        public Note Find(Expression<Func<Note, bool>> filter)
        {
            return repo_note.Find(filter);
        }

        public int Insert(Note entity)
        {
            return repo_note.Insert(entity);
        }

        public int Update(Note entity)
        {
            return repo_note.Update(entity);
        }

        public int Delete(Note entity)
        {
            return repo_note.Delete(entity);
        }

        public int Save()
        {
            return repo_note.Save();
        }
    }
}
