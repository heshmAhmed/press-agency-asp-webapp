using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace press_agency_asp_webapp.Controllers
{
    public class EditorController : Controller
    {
        // GET: Editor
        public ActionResult CreatePost()
        {
            return View();
        }
    }
}