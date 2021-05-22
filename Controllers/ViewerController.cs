using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace press_agency_asp_webapp.Controllers
{
    public class ViewerController : Controller
    {
        // GET: Viewer
        public ActionResult Wall()
        {
            return View();
        }
    }
}