using DonationsWebApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DonationsWebApplication.web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, string password)
        {
            UserRepository rep = new UserRepository(Properties.Settings.Default.ConStr);
            rep.Add(user, password);
            return Redirect("/home/index");
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string password, string email)
        {
            UserRepository rep = new UserRepository(Properties.Settings.Default.ConStr);
            if(!rep.IsMatch(password, email))
            {
                return Redirect("/user/SignIn");
            }
            FormsAuthentication.SetAuthCookie(email, true);
            return Redirect("/home/index");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}