using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.Services;
using press_agency_asp_webapp.Util;
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

        //[HttpGet]
        //public ActionResult Posts()
        //{
        //    return View(ActorService.GetPosts);
        //}

        [HttpGet]
        public ActionResult Profile(Actor actor)
        {
            return View(Mapping.MapToUserViewMoel(actor));
        }

        [HttpPost]
        public ActionResult Update(UserViewModel userViewModel)
        {
            
            return Json(new { result = 0 });
        }


        [HttpGet]
        public ActionResult Register()
        {
            UserViewModel userViewModel = new UserViewModel
            {
                UserTypes = Db.UserTypes.ToList()

            };
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Register(UserViewModel userViewModel)
        {
            ActorService.CreateActor(userViewModel);
            return RedirectToAction("LogIn");
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LogIn(IdentityViewModel identityViewModel)
        {
            Actor actor;
            actor = GeneralUtil.ISEmail(identityViewModel.Identity) ? ActorService.FindActor(identityViewModel) : ActorService.FindEditor(identityViewModel);
            if (actor != null)
            {
                Debug.WriteLine(actor.Email);
                Debug.WriteLine(actor.Password);
                Session["user"] = new
                {
                    id = actor.Id,
                    usertype = actor.UserType,
                };
                return RedirectToAction("posts", "Actor", actor);
            }
            ViewBag.error = "You have entered invalid data !!";
       
            return View("LogIn");
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