using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.Services;
using press_agency_asp_webapp.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace press_agency_asp_webapp.Controllers
{
    public class ActorController : Controller
    {
        CodeFContext Db = new CodeFContext();
        ActorService ActorService = ActorService.CreateActorService(new CodeFContext());

        
        [HttpGet]
        public ActionResult Register()
        {
            ActorTypeViewModel ActorTypeViewModel = new ActorTypeViewModel
            {
                UserTypes = Db.UserTypes.ToList()

            };
            return View(ActorTypeViewModel);
        }

        [HttpPost]
        public ActionResult Register(ActorTypeViewModel actorTypeViewModel)
        {
            Debug.WriteLine(actorTypeViewModel.Actor.FirstName);
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult CheckEmail(string email)
        {
            return Json(new
            {
                result = ActorService.CheckEmail(email)
            });

        }

        [HttpPost]
        public ActionResult CheckUserName(string username)
        {
            return Json(new
            {
                result = ActorService.CheckUserName(username),
            }); ; 

        }
    }
}