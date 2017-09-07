using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DonationsWebApplication.data;
using DonationsWebApplication.web.Models;

namespace DonationsWebApplication.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                UserRepository repo = new UserRepository(Properties.Settings.Default.ConStr);
                User user = repo.GetByUserName(User.Identity.Name);
                if (user.IsAdmin)
                {
                    return Redirect("/Admin/index");
                    
                }
                return RedirectToAction("home");
            }
            return View();
        }
        [Authorize]
        public ActionResult Home()
        {
             return View();
        }
        [Authorize]
        public ActionResult Application()
        {
            ApplicationViewModel avm = new ApplicationViewModel();
            avm.Categories = new CategoryRepository(Properties.Settings.Default.ConStr).GetCategories();
            return View(avm);
        }
        [HttpPost]
        public ActionResult Application(Application application, string userName)
        {
            ApplicationRepository appRep = new ApplicationRepository(Properties.Settings.Default.ConStr);
            UserRepository userRep = new UserRepository(Properties.Settings.Default.ConStr);
            application.UserId = userRep.GetByUserName(User.Identity.Name).Id;
            appRep.Add(application);
            return Redirect("/home/home");
        }
        public ActionResult History(int? UserId)
        {
            HstoryViewModel hvm = new HstoryViewModel();
            ApplicationRepository Rep = new ApplicationRepository(Properties.Settings.Default.ConStr);
            UserRepository rep2 = new UserRepository(Properties.Settings.Default.ConStr);
            int userId = UserId == null ? (rep2.GetByUserName(User.Identity.Name).Id) : (int)UserId;
            hvm.Applications = Rep.GetApplications(userId);
            return View(hvm);
        }
    }
}