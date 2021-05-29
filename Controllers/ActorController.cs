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
        public ActionResult Profile()
        {
            int id = (int)Session["userId"];
            return Session["UserType"] == "editor" ? View(Mapping.MapToUserViewModel(ActorService.FindEditor((int)Session["userId"])))
                 : View(Mapping.MapToUserViewModel(ActorService.FindActor((int)Session["userId"])));  
        }

        [HttpPost]
        public ActionResult Update(UserViewModel userViewModel)
        {
            userViewModel = ActorService.UpdateAcctor((int)Session["userId"], userViewModel);
            Session["userFullName"] = userViewModel.FirstName + " " + userViewModel.LastName;
            return Json(userViewModel);
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
                Session["userId"] = actor.Id;
                Session["userType"] = actor.UserType.Name;
                Session["userFullName"] = actor.FirstName + " " + actor.LastName;
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