using Blog_BusinessLayer;
using Blog_WebUI.Filter;
using Blog_WebUI.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Blog_WebUI.Controllers
{
    [HandleException]
    public class CommentController : Controller
    {
        // GET: Comment
        NoteManager noteManager = new NoteManager();
        CommentManager commentManager = new CommentManager();
        public ActionResult ShowNoteComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialComment", note.Comments);
        }
        [Auth]
        [HttpPost]
        public ActionResult Edit(int? id, string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = commentManager.Find(x => x.Id == id);

            if (comment == null)
            {
                return new HttpNotFoundResult();
            }
            comment.Text = text;

            if (commentManager.Update(comment) > 0)
            {

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = commentManager.Find(x => x.Id == id);

            if (comment == null)
            {
                return new HttpNotFoundResult();
            }

            if (commentManager.Delete(comment) > 0)
            {

                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new { data = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [Auth]
        [HttpPost]
        public ActionResult Create(Comment comment, int? noteId)
        {
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                if (noteId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Note note = noteManager.Find(x => x.Id == noteId);
                if (note == null)
                {
                    return new HttpNotFoundResult();
                }

                comment.Note = note;
                comment.Owner = CurrentSession.User;
                comment.ModifiedDate = DateTime.Now;
                comment.CreatedDate = DateTime.Now;
                comment.ModifiedUserName = CurrentSession.User.Username;

                if (commentManager.Insert(comment) > 0)
                {

                    return Json(new { data = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }
    }
}
