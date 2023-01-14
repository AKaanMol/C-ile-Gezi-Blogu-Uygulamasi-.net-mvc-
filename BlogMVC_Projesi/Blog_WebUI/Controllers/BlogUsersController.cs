using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog_BusinessLayer;
using Blog_WebUI.Filter;
using Entities;

namespace Blog_WebUI.Controllers
{
    [HandleException]
    [Auth]
    [AuthAdmin]
    public class BlogUsersController : Controller
    {
        private BlogUserManager userManager = new BlogUserManager();


        public ActionResult Index()
        {
            return View(userManager.List());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUser blogUser = userManager.Find(x => x.Id == id);
            if (blogUser == null)
            {
                return HttpNotFound();
            }
            return View(blogUser);
        }

        // GET: BlogUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogUsers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogUser blogUser)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<BlogUser> blResult = userManager.Insert(blogUser);
                if (blResult.Erorrs.Count > 0)
                {
                    blResult.Erorrs.ForEach(x => ModelState.AddModelError("", x));
                    return View(blogUser);
                }

                return RedirectToAction("Index");
            }

            return View(blogUser);
        }

        // GET: BlogUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUser blogUser = userManager.Find(x => x.Id == id);
            if (blogUser == null)
            {
                return HttpNotFound();
            }
            return View(blogUser);
        }

        // POST: BlogUsers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogUser blogUser)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<BlogUser> blResult = userManager.Update(blogUser);
                if (blResult.Erorrs.Count > 0)
                {
                    blResult.Erorrs.ForEach(x => ModelState.AddModelError("", x));
                    return View(blogUser);

                }


                return RedirectToAction("Index");
            }
            return View(blogUser);
        }

        // GET: BlogUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUser blogUser = userManager.Find(x => x.Id == id);
            if (blogUser == null)
            {
                return HttpNotFound();
            }
            return View(blogUser);
        }

        // POST: BlogUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogUser blogUser = userManager.Find(x => x.Id == id);
            userManager.Delete(blogUser);
            return RedirectToAction("Index");
        }


    }
}
