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

namespace press_agency_asp_webapp.Controllers
{
    public class AdminController : Controller
    {
        private AdminService adminService = AdminService.CreateAdminService(CodeFContext.CreateCodeFContext());

        [HttpGet]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult PostRequests()
        {
            List<Post> postsRequests = adminService.GetPostRequests();
            return View(new WallViewModel { Posts = postsRequests });
        }

        [HttpGet]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult AcceptPost(int postId)
        {
            adminService.AcceptPost(postId);
            return Json(new { result = true });
        }

        [HttpGet]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult RejectPost(int postId)
        {
            adminService.RejectPost(postId);
            return Json(new { result = true });
        }

        public ActionResult Posts()
        {
            return View("Posts", adminService.GetPosts());
        }

        [HttpGet]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult UpdatePost(int id)
        {
            Post post = adminService.GetPost(id);
            CreatePostViewModel createPostView = new CreatePostViewModel
            {
                Id = post.Id,
                Body = post.Body,
                Title = post.Title,
                No_Likes = post.No_likes,
                No_DisLikes = post.No_dislikes,
                No_Views = post.No_views,
                PostTypeId = post.PostTypeId,
                PostTypes = adminService.GetPostTypes()
            };

            return View(createPostView);
        }


        [HttpPost]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult UpdatePost(CreatePostViewModel createPostViewModel, HttpPostedFileBase postImage)
        {
            string path = Server.MapPath("~/Content/images");
            string imgName = GeneralUtil.upload_image(path, postImage);
            Post post = new Post
            {
                Id = createPostViewModel.Id,
                Title = createPostViewModel.Title,
                Body = createPostViewModel.Body,
                No_views = createPostViewModel.No_Views,
                No_dislikes = createPostViewModel.No_DisLikes,
                No_likes = createPostViewModel.No_Likes,
            };
            post.ImagePath = imgName;
            post.PostTypeId = createPostViewModel.PostTypeId;
            adminService.UpdatePost(post);
            return RedirectToAction("Posts");
        }
        [HttpGet]
        [CustomAuthorize(Roles ="admin")]
        public ActionResult Users()
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            List<Actor> actors = adminService.GetUsers();
            foreach (Actor actor in actors)
            {
                userViewModels.Add(new UserViewModel
                {
                    FirstName = actor.FirstName,
                    LastName = actor.LastName,
                    Email = actor.Email,
                    Phone = actor.Phone,
                    UserName = actor.UserType.Name == "editor" ? ((Editor)actor).Username : null,
                    UserTypeName = actor.UserType.Name,
                    CreateDate = actor.CreateDate,
                    Id = actor.Id
                });
            }
            return View(userViewModels);
        }

        [CustomAuthorize(Roles = "admin")]
        [HttpGet]
        public ActionResult UserProfile(int id)
        {
            Actor actor = adminService.FindActor(id);
            return View("Profile", new UserViewModel
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Email = actor.Email,
                ImagePath = actor.ImagePath,
                Phone = actor.Phone,
                UserName = actor.UserType.Name == "editor" ? ((Editor)actor).Username : null,
                UserTypeName = actor.UserType.Name,
            }); 
        }

        [HttpGet]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult DeleteUser(int userId)
        {
            adminService.DeleteUser(userId);
            return Json(true);
        }
    }
}