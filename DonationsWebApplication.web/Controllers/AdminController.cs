using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DonationsWebApplication.web.Models;
using DonationsWebApplication.data;

namespace DonationsWebApplication.web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.Name != "miriam@admin.com")
            {
                return RedirectToAction("Index", "Home");
            }
            PendingViewModel pvm = new PendingViewModel();
            CategoryRepository rep = new CategoryRepository(Properties.Settings.Default.ConStr);
            pvm.Categories = rep.GetCategories();
            return View(pvm);
        }
        [Authorize]
        [HttpPost]
        public void ApplicationAction(int ApplicationId, bool IsAccepted)
        {
            if (User.Identity.Name != "miriam@admin.com")
            {
                return;
            }
            ApplicationRepository repo = new ApplicationRepository(Properties.Settings.Default.ConStr);
            repo.AplicationAction(ApplicationId, IsAccepted);
        }
       
       public ActionResult GetPendingApplications(int? categoryId)
       {
            ApplicationRepository repo = new ApplicationRepository(Properties.Settings.Default.ConStr);
            var result = repo.GetPending(categoryId).Select(a => new
            {
                id = a.Id,
                category = a.Category.Name,
                firstName = a.User.FirstName,
                lastName = a.User.LastName,
                email = a.User.Email,
                amount = a.Amount,
                userId = a.UserId
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
}