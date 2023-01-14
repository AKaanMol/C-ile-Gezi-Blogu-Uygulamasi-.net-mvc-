using Blog_BusinessLayer;
using Entities.ViewModels;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog_WebUI.Models;
using Blog_WebUI.Filter;

namespace Blog_WebUI.Controllers
{
    [HandleException]
    public class HomeController : Controller
    {

        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private BlogUserManager blogUserManager = new BlogUserManager();
        public ActionResult Index()
        {

            return View(noteManager.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedDate).ToList());
        }
        public ActionResult MostLiked()
        {



            return View("Index", noteManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }


        public ActionResult SelectCategory(int id)
        {

            Category category = categoryManager.Find(x => x.Id == id);

            return View("Index", category.Notes.OrderByDescending(x => x.ModifiedDate).ToList());
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                BusinessLayerResult<BlogUser> blResult = blogUserManager.LoginUser(model);

                if (blResult.Erorrs.Count > 0)
                {
                    blResult.Erorrs.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                CurrentSession.Set<BlogUser>("login", blResult.Result);

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Logout()
        {

            CurrentSession.Clear();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                BusinessLayerResult<BlogUser> blResult = blogUserManager.RegisterUser(model);
                if (blResult.Erorrs.Count > 0)
                {
                    blResult.Erorrs.ForEach(x => ModelState.AddModelError("", x));

                    return View(model);

                }

                return RedirectToAction("RegisterSuccess");
            }
            return View(model);
        }
        public ActionResult RegisterSuccess()
        {
            return View();
        }
        public ActionResult UserActivate(Guid id)
        {

            BusinessLayerResult<BlogUser> blResult = blogUserManager.UserActivate(id);
            if (blResult.Erorrs.Count > 0)
            {
                TempData["errors"] = blResult.Erorrs;

                return RedirectToAction("ActivateUserCancel");
            }

            return RedirectToAction("ActivateUserOk");
        }
        public ActionResult ActivateUserOk()
        {
            return View();
        }
        public ActionResult ActivateUserCancel()
        {
            List<string> errors = null;
            if (TempData["errors"] != null)
            {
                errors = TempData["errors"] as List<string>;
            }

            return View(errors);
        }
        [Auth]
        public ActionResult ShowProfile()
        {
            BlogUser currentUser = CurrentSession.User;

            BusinessLayerResult<BlogUser> blResult = blogUserManager.GetUserById(currentUser.Id);
            if (blResult.Erorrs.Count > 0)
            {

                return View("ProfileLoadError", blResult.Erorrs);
            }

            return View(blResult.Result);
        }
        [Auth]
        [HttpGet]
        public ActionResult EditProfile()
        {
            BlogUser currentUser = CurrentSession.User;

            BusinessLayerResult<BlogUser> blResult = blogUserManager.GetUserById(currentUser.Id);
            if (blResult.Erorrs.Count > 0)
            {
                return View("ProfileLoadError", blResult.Erorrs);
            }

            return View(blResult.Result);
        }
        [Auth]
        [HttpPost]
        public ActionResult EditProfile(BlogUser user, HttpPostedFileBase ProfileImage)
        {

            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null && (
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/png"))
                {
                    string fileName = $"user_{user.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{fileName}"));
                    user.UserProfileImage = fileName;
                }

                BusinessLayerResult<BlogUser> blResult = blogUserManager.UpdateProfile(user);
                if (blResult.Erorrs.Count > 0)
                {
                    blResult.Erorrs.ForEach(x => ModelState.AddModelError("", x));
                    return View(blResult.Result);

                }

                CurrentSession.Set<BlogUser>("login", blResult.Result);
                return RedirectToAction("ShowProfile");
            }

            return View(user);
        }
        [Auth]
        public ActionResult DeleteProfile()
        {
            BlogUser currentUser = CurrentSession.User;

            BusinessLayerResult<BlogUser> blResult = blogUserManager.DeleteUser(currentUser.Id);
            if (blResult.Erorrs.Count > 0)
            {
                blResult.Erorrs.ForEach(x => ModelState.AddModelError("", x));
                return View("ProfileLOadError", blResult.Erorrs);
            }
            CurrentSession.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
        public ActionResult HasError()
        {
            return View();
        }
    }
}