using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.Security;
using press_agency_asp_webapp.Services;
using press_agency_asp_webapp.Util;
using press_agency_asp_webapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace press_agency_asp_webapp.Controllers
{
    public class ActorController : Controller
    {
        CodeFContext Db = CodeFContext.CreateCodeFContext();
        ActorService ActorService = ActorService.CreateActorService(CodeFContext.CreateCodeFContext());

        [CustomAuthorize(Roles = "editor,admin,viewer")]
        [HttpGet]
        public ActionResult Profile()
        {
            return Session["UserType"].Equals("editor") ? View(Mapping.MapToUserViewModel(ActorService.FindEditor(SessionPersister.userId)))
                 : View(Mapping.MapToUserViewModel(ActorService.FindActor(SessionPersister.userId)));
        }

        [CustomAuthorize(Roles = "editor,admin,viewer")]
        [HttpPost]
        public ActionResult Update(UserViewModel userViewModel)
        {
            userViewModel = ActorService.UpdateAcctor(SessionPersister.userId, userViewModel);
            Session["userFullName"] = userViewModel.FirstName + " " + userViewModel.LastName;
            return Json(userViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            if (!string.IsNullOrEmpty(SessionPersister.Identity) && !SessionPersister.userType.Equals("admin"))
            {
                return RedirectToAction("wall", "viewer");
            }
            UserViewModel userViewModel = new UserViewModel
            {
                UserTypes = Db.UserTypes.ToList()

            };
            return View(userViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(UserViewModel userViewModel, HttpPostedFileBase userImage)
        {
            string path = Server.MapPath("~/Content/images");
            string imgName = GeneralUtil.upload_image(path, userImage);
            userViewModel.ImagePath = imgName;
            ActorService.CreateActor(userViewModel);
            if (SessionPersister.userType.Equals("admin"))
            {
                return RedirectToAction("Users","Admin");
            }
            return RedirectToAction("LogIn");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogIn()
        {
            if (!string.IsNullOrEmpty(SessionPersister.Identity))
            {
                return RedirectToAction("wall", "viewer");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogIn(IdentityViewModel identityViewModel)
        {
            Actor actor = ActorService.FindActor(identityViewModel.Identity, identityViewModel.Password);
            if (actor == null)
            {
                ViewBag.error = "Account's Invalid";
                return View("LogIn");

            }
            SessionPersister.Identity = identityViewModel.Identity;
            SessionPersister.userId = actor.Id;
            SessionPersister.userType = actor.UserType.Name;
            SessionPersister.userFullName = actor.FirstName + " " + actor.LastName;

            return RedirectToAction("Wall", "Viewer");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PopUpLogIn(IdentityViewModel identityViewModel)
        {
            Actor actor = ActorService.FindActor(identityViewModel.Identity, identityViewModel.Password);
            if (actor == null)
                return Json(new { result = false });
            SessionPersister.Identity = identityViewModel.Identity;
            SessionPersister.userId = actor.Id;
            SessionPersister.userType = actor.UserType.Name;
            SessionPersister.userFullName = actor.FirstName + " " + actor.LastName;
            return Json(new { result = true });
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

        [CustomAuthorize(Roles ="admin,editor,viewer")]
        [HttpGet]
        public ActionResult LogOut()
        {
            SessionPersister.userFullName = string.Empty;
            SessionPersister.userId = 0;
            SessionPersister.userType = string.Empty;
            SessionPersister.Identity = string.Empty;
            return RedirectToAction("Wall", "Viewer");
        }

        public ActionResult UpdateImage(HttpPostedFileBase userImage)
        {
            string path = Server.MapPath("~/Content/images");
            string imgName = GeneralUtil.upload_image(path, userImage);
            ActorService.UpdateImage(SessionPersister.userId, imgName);
            return RedirectToAction("Profile");
        }

      
    }
}