using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Services
{
    public class AdminService
    {
        CodeFContext Db;
        private static AdminService Instanse = null;
        private AdminService(CodeFContext Db) { this.Db = Db; }

        public static AdminService CreateAdminService(CodeFContext codeFContext)
        {
            if (Instanse == null)
                Instanse = new AdminService(codeFContext);
            return Instanse;
        }

        public List<Post> GetPostRequests()
        {
            return Db.Posts.Where(post => post.State == null).ToList();
        }

        public void AcceptPost(int postId)
        {
            Post post = Db.Posts.Where(row => row.Id == postId).FirstOrDefault();
            post.State = true;
            Db.Entry(post).State = EntityState.Modified;
            Db.SaveChanges();
        }
        public void RejectPost(int postId)
        {
            Post post = Db.Posts.Where(row => row.Id == postId).FirstOrDefault();
            post.State = false;
            Db.Entry(post).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public List<Post> GetPosts()
        {
            return Db.Posts.ToList();
        }

        public Post GetPost(int postId)
        {
            return Db.Posts.Find(postId);
        }

        public List<PostType> GetPostTypes()
        {
            return Db.PostTypes.ToList();
        }

        public bool UpdatePost(Post post)
        {
            try
            {
                Post post1 = Db.Posts.Find(post.Id);
                post1.Title = post.Title;
                post1.PostTypeId = post.PostTypeId;
                post1.Body = post.Body;
                post1.No_likes = post.No_likes;
                post1.No_dislikes = post.No_dislikes;
                post1.No_views = post.No_views;
                post1.ImagePath = post.ImagePath == null ? post1.ImagePath : post.ImagePath;
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Actor> GetUsers()
        {
            return Db.Actors.ToList();
        }

        public Actor FindActor(int id)
        {
           return Db.Actors.Find(id);
        }

        public void DeleteUser(int userId)
        {
            Actor actor = Db.Actors.Find(userId);
            if (actor.UserType.Name.Equals("viewer"))
            {
                DeleteUserSavedPosts((Viewer)actor);
                DeleteActorQuestions((Viewer)actor);
                DeleteActorInteractios((Viewer)actor);
            }
            else if (actor.UserType.Name.Equals("editor"))
            {
                DeleteAcotrPosts((Editor)actor);
            }
            Db.Actors.Remove(actor);
            Db.SaveChanges();
        }
        
        public void DeleteUserSavedPosts(Viewer viewer)
        {
            foreach(Post post in viewer.SavedPosts.ToList())
            {
                viewer.SavedPosts.Remove(post);
            }
            Db.SaveChanges();
        }

        public void DeleteActorQuestions(Viewer viewer)
        {
            foreach (Question q in viewer.Questions.ToList())
            {
                if (q.ViewerId == viewer.Id)
                    q.ViewerId = 0;
            }
            Db.SaveChanges();
        }

        public void DeleteActorInteractios(Viewer viewer)
        {
            foreach (Interaction interaction in viewer.Interactions.ToList())
            {
                if (interaction.ViewerId == viewer.Id)
                    interaction.ViewerId = 0;
            }
            Db.SaveChanges();
        }
        public void DeleteAcotrPosts(Editor editor)
        {
            foreach (Post post in editor.Posts.ToList())
            {
                editor.Posts.Remove(post);
            }
            Db.SaveChanges();
        }
    }
}