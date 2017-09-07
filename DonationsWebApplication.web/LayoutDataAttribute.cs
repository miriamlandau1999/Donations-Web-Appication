using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DonationsWebApplication.data;

namespace DonationsWebApplication.web
{
    public class LayoutDataAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var db = new UserRepository(Properties.Settings.Default.ConStr);
                var user = db.GetByUserName(filterContext.HttpContext.User.Identity.Name);
                filterContext.Controller.ViewBag.User = user;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}