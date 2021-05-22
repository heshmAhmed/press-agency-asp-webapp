using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace press_agency_asp_webapp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Posts()
        {
            return View();
        }

        public ActionResult Requests()
        {
            return View();
        }

    
    }
}