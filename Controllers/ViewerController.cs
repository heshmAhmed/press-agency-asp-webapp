using Newtonsoft.Json;
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
    public class ViewerController : Controller
    {
        private ViewerService viewerService = ViewerService.CreateViewerService(CodeFContext.CreateCodeFContext());
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Wall()
        {


            WallViewModel wallViewModel = new WallViewModel
            {
                Posts = viewerService.GetPosts(),
                Interactions = SessionPersister.userId == 0 || SessionPersister.userType != "viewer" ? null : GetInteractions(),
                SavedPostsIds = SessionPersister.userId == 0 || SessionPersister.userType != "viewer" ? null : GetSavedPostsIds()
            };
            return View(wallViewModel);
        }

        [CustomAuthorize(Roles = "viewer")]
        [HttpGet]
        public ActionResult Like(int postId)
        {

            viewerService.Interact(SessionPersister.userId, postId, true);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "viewer")]
        [HttpGet]
        public ActionResult DisLike(int postId)
        {
            viewerService.Interact(SessionPersister.userId, postId, false);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "viewer")]
        [HttpGet]
        public ActionResult RemoveInteraction(int postId)
        {
            return Json(viewerService.RemoveInteraction(SessionPersister.userId, postId), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Search(string searchitem)
        {
            Debug.WriteLine(searchitem);
            List<Post> posts = viewerService.SearchForPosts(searchitem).ToList();
            foreach (Post post in posts)
            {
                Debug.WriteLine(post.Editor.FirstName);
                Debug.WriteLine(post.Title);
                Debug.WriteLine(post.Editor.LastName);
            }
            WallViewModel wall = new WallViewModel
            {
                Posts = posts,
                Interactions = GetInteractions()
            };
            return View("Wall", wall);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Post(int id)
        {
            Post post = viewerService.FindPost(id);
            InteractionViewModel interactionViewModel = null;
            bool isSaved = false;

            if (SessionPersister.userId != 0 && SessionPersister.userType == "viewer") {
                Interaction interaction = post.Interactions.Where(row => row.ViewerId == SessionPersister.userId).FirstOrDefault();
                if (interaction == null)
                    interactionViewModel = null;
                else
                    interactionViewModel = new InteractionViewModel
                    {
                        PostID = id,
                        ViewerID = SessionPersister.userId,
                        IsLike = interaction.IsLike
                    };

                isSaved = post.SavedPosts.Where(row => row.Id == SessionPersister.userId).FirstOrDefault() != null;
            }

            return View("Post", new PostViewModel
            {
                Post = post,
                Interaction = interactionViewModel,
                Qustions = GeneralUtil.GetQustionViewModels(post),
                IsSaved = isSaved
            });
        }


        [HttpPost]
        [CustomAuthorize(Roles = "viewer")]
        public ActionResult AddQuestion(Question question)
        {
            question.CreateDate = DateTime.Now;
            question.ViewerId = SessionPersister.userId;
            question = viewerService.AddQuestion(question);
            return Json(new
            {
                question = new QuestionViewModel
                {
                    Id = question.Id,
                    Title = question.Title,
                    CreateDate = question.CreateDate,
                    PostId = question.PostId,
                    ViewerId = question.ViewerId
                },
                state = true
            });
        }


        [CustomAuthorize(Roles = "viewer")]
        [HttpGet]
        public ActionResult SavePost(int postId)
        {
            viewerService.SavePost(SessionPersister.userId, postId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private List<int> GetSavedPostsIds()
        {
            List<Post> savedPosts = viewerService.GetSavedPosts(SessionPersister.userId);
            List<int> savedPostsIds = new List<int>();
            foreach (Post post in savedPosts)
            {
                savedPostsIds.Add(post.Id);
            }
            return savedPostsIds;
        }

        [CustomAuthorize(Roles = "viewer")]
        [HttpGet]
        public ActionResult SavedPosts()
        {
            List<Post> savedPosts = viewerService.GetSavedPosts(SessionPersister.userId);
            List<InteractionViewModel> interactions = GetInteractions(savedPosts);
            return View("SavedPosts", new SavedPostsViewModel
            {
                Interactions = interactions,
                SavedPosts = savedPosts
            });
        }

        [HttpGet]
        [CustomAuthorize(Roles ="viewer")]
        public ActionResult UnSavePost(int postId)
        {
            viewerService.unSavePost(postId, SessionPersister.userId);
            return Json(new { result =  true });
        }

        private List<InteractionViewModel> GetInteractions()
        {
            List<Interaction> interactions = (List<Interaction>)viewerService.GetInteractions(SessionPersister.userId);
            List<InteractionViewModel> interactionViews = new List<InteractionViewModel>();
            foreach (Interaction element in interactions)
            {
                interactionViews.Add(new InteractionViewModel { PostID = element.PostId, IsLike = element.IsLike, ViewerID = element.ViewerId });
            }
            return interactionViews;
        }


        private List<InteractionViewModel> GetInteractions(List<Post> posts)
        {
            List<InteractionViewModel> interactionViewModels = new List<InteractionViewModel>();
            foreach(Post post in posts)
            {
                Interaction interaction = post.Interactions.Where(row => row.ViewerId == SessionPersister.userId).FirstOrDefault();
                if(interaction != null)
                    interactionViewModels.Add(new InteractionViewModel
                    {
                        PostID = post.Id,
                        IsLike = interaction.IsLike,
                        ViewerID = interaction.ViewerId
                    });
            }
            return interactionViewModels;
        } 

    }
}