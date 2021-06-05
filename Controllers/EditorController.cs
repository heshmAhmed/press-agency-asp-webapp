using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.Security;
using press_agency_asp_webapp.Services;
using press_agency_asp_webapp.Util;
using press_agency_asp_webapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace press_agency_asp_webapp.Controllers
{
    public class EditorController : Controller
    {
        private EditorService editorService = EditorService.CreateEditorService(CodeFContext.CreateCodeFContext());

        [HttpGet]
        [CustomAuthorize(Roles = "editor")]
        public ActionResult UpdatePost(int id)
        {
            Post post = editorService.GetPost(id);
            CreatePostViewModel createPostView = new CreatePostViewModel
            {
                Id = id,
                Body = post.Body,
                Title = post.Title,
                PostTypeId = post.PostTypeId,
                PostTypes = editorService.GetPostTypes()
            };

            return View(createPostView);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "editor")]
        public ActionResult UpdatePost(CreatePostViewModel createPostViewModel, HttpPostedFileBase postImage)
        {
            string path = Server.MapPath("~/Content/images");
            string imgName = GeneralUtil.upload_image(path, postImage);
            editorService.UpdatePost(new Post
            {
                Id = createPostViewModel.Id,
                Title = createPostViewModel.Title,
                Body = createPostViewModel.Body,
                PostTypeId = createPostViewModel.PostTypeId,
                ImagePath = imgName
            });
            return RedirectToAction("History");
        }


        // GET: Editor
        [HttpGet]
        [CustomAuthorize(Roles ="editor,admin")]
        public ActionResult CreatePost()
        {
            CreatePostViewModel createPostViewModel = new CreatePostViewModel
            {
                PostTypes = editorService.GetPostTypes()
            };
            return View(createPostViewModel);
        }
        
        [HttpPost]
        [CustomAuthorize(Roles = "editor,admin")]
        public ActionResult CreatePost(CreatePostViewModel createPostViewModel, HttpPostedFileBase postImage)
        {
            string path = Server.MapPath("~/Content/images");
            string imgName = GeneralUtil.upload_image(path, postImage);
            if (imgName != null)
            {
                editorService.CreatePost(new Post
                {
                    Title = createPostViewModel.Title,
                    Body = createPostViewModel.Body,
                    No_dislikes = 0,
                    No_likes = 0,
                    No_views = 0,
                    CreateDate = DateTime.Now,
                    ImagePath = imgName,
                    EditorId = SessionPersister.userId,
                    PostTypeId = createPostViewModel.PostTypeId,
                });
                ViewBag.state = "Post is Created";
            }
            else
                ViewBag.state = "Post is not Created";
            return View("CreatePost", new CreatePostViewModel{PostTypes = editorService.GetPostTypes()} );
        }
    

        [HttpGet]
        [CustomAuthorize(Roles ="editor")]
        public ActionResult Questions()
        {
            ICollection<Post> unAnsweredPosts = editorService.GetUnAnsweredPosts(SessionPersister.userId);
            List<PostViewModel> postViewModels = new List<PostViewModel>();
            foreach(Post post in unAnsweredPosts)
            {
                postViewModels.Add(new PostViewModel
                {
                    Post = post,
                    Qustions = GeneralUtil.GetQustionViewModels(post),
                    IsSaved = false,
                    Interaction = null
                });
            }

            return View("Questions", postViewModels);

        }

        [HttpPost]
        [CustomAuthorize(Roles ="editor")]
        public ActionResult AddAnswer(int questionId, string answer)
        {
            Debug.WriteLine("a7a");
            Debug.WriteLine(questionId);
            Debug.WriteLine(answer);
            editorService.UpdateAnswer(questionId, answer);
            return Json(new { result  = true});
        }

        [HttpGet]
        [CustomAuthorize(Roles = "editor")]
        public ActionResult History()
        {
            return View("History", editorService.GetPosts(SessionPersister.userId));
        }
        
        [HttpGet]
        [CustomAuthorize(Roles ="editor,admin")]
        public ActionResult DeletePost(int postId)
        {
            editorService.DeletePost(postId, SessionPersister.userId);
            return Json(true);
        }
    }
}