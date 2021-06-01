using Newtonsoft.Json;
using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.Security;
using press_agency_asp_webapp.Services;
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
        private ViewerService viewerService = ViewerService.CreateViewerService(new CodeFContext());
        private ActorService actorService = ActorService.CreateActorService(new CodeFContext());
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Wall()
        {
            WallViewModel wallViewModel = new WallViewModel
            {
                Posts = viewerService.GetPosts(),
                Interactions = SessionPersister.userId == 0 ? null : GetInteractions(),
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
            return Json(viewerService.RemoveInteraction(4, postId), JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "editor,viewer,admin")]
        [HttpPost]
        public ActionResult Search(string searchitem)
        {
            Debug.WriteLine(searchitem);
            List<Post> posts = viewerService.SearchForPosts(searchitem).ToList();
            foreach(Post post in posts)
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

        [HttpGet]
        [CustomAuthorize(Roles ="viewer")]
        public ActionResult Post(int id)
        {
            Post post = viewerService.FindPost(id);
            return View("Post", new PostViewModel
            {
                Post = post,
                Interactions = GetInteractions(),
                Qustions = GetQustionViewModels(post)
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

        private ICollection<QuestionViewModel> GetQustionViewModels(Post post)
        {
            List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();
            foreach(Question question in post.Questions)
            {
                questionViewModels.Add(new QuestionViewModel
                {
                    Id = question.Id,
                    PostId = question.PostId,
                    ViewerId = question.ViewerId,
                    Title = question.Title,
                    Answer = question.Answer,
                    CreateDate = question.CreateDate
                });
            }
            return questionViewModels;
        }
    }
}