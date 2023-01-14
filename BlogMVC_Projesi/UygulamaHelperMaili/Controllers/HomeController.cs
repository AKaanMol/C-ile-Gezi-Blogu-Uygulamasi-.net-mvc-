using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UygulamaHelperMaili.Models;

namespace UygulamaHelperMaili.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            MailHelper.SendMail("<h2>Merhaba<h2><br> ilk deneme maili", "kaanmol@gmail.com", "İlk Mail");

            return View();
        }
    }
}