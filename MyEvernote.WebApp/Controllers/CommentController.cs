using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.Bll;
using MyEvernote.Entities;

namespace MyEvernote.WebApp.Controllers
{
    public class CommentController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        // GET: Comment
        public ActionResult ShowNoteComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = noteManager.GetAllNotesQueryable().AsQueryable().Include(s=>s.Comment).FirstOrDefault(x=>x.Id == id);

            if (note == null)
            {
                return new HttpNotFoundResult();
            }
            
            return PartialView("_PartialComment",note.Comment);
        }
    }
}