using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace press_agency_asp_webapp.Services
{
    public class ViewerService
    {
        CodeFContext Db;
        private static ViewerService Instanse = null;
        private ViewerService(CodeFContext Db) { this.Db = Db; }

        public static ViewerService CreateViewerService(CodeFContext codeFContext)
        {
            if (Instanse == null)
                Instanse = new ViewerService(codeFContext);
            return Instanse;
        }

        public ICollection<Post> GetPosts()
        {
            return Db.Posts.Where(post => post.State == true).ToList();
        }

        public Interaction Interact(int viewerId,int postId, bool islike)
        {
            Post post = Db.Posts.Find(postId);
            Interaction interaction = Db.Interactions.Where(action => action.PostId == postId && action.ViewerId == viewerId)
                .FirstOrDefault();
            if (interaction == null)
            {
                interaction = new Interaction
                {
                    IsLike = islike,
                    CreateDate = DateTime.Now,
                    ViewerId = viewerId,
                    PostId = postId
                };
                Db.Interactions.Add(interaction);
            }
            else
                interaction.IsLike = islike;
            try
            {
                HandlePostNoInteraction(post, islike);
                Db.SaveChanges();
                return interaction;
            }catch(Exception e)
            {
                return null;
            }
        }

        private void HandlePostNoInteraction(Post post, bool islike)
        {
            if (islike)
            {
                post.No_likes++;
                if (post.No_dislikes > 0)
                    post.No_dislikes--;
            }
            else
            {
                post.No_dislikes++;
                if (post.No_likes > 0)
                    post.No_likes--;
            }
        }

        public bool RemoveInteraction(int viewerId, int postId)
        {
            try
            {
                Interaction interaction = Db.Interactions.Where(row => row.ViewerId == viewerId && row.PostId == postId).FirstOrDefault();
                Debug.WriteLine(viewerId);
                Debug.WriteLine(postId);
                Post post = Db.Posts.Find(postId);
                if (interaction.IsLike)
                    post.No_likes = post.No_likes == 0 ? 0 : post.No_likes - 1;
                else
                    post.No_dislikes = post.No_dislikes == 0 ? 0 : post.No_dislikes - 1;
                Db.Interactions.Remove(interaction);
                Db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public ICollection<Interaction> GetInteractions(int viewerId)
        {
            return Db.Interactions.Where(row => row.ViewerId == viewerId).ToList();
        }

        public ICollection<Post> SearchForPosts(string searchitem)
        {
            return Db.Posts.Where(post => post.Editor.Username == searchitem || post.PostType.Name == searchitem || post.Title.Contains(searchitem)).ToList();
        }

        public Post FindPost(int postId) {
            Post post = Db.Posts.Find(postId);
            if (post != null) {
                post.No_views++;
                Db.SaveChanges();
            }
            return post;
        }

        public Question AddQuestion(Question question)
        {
            try
            {
                Db.Questions.Add(question);
                Db.SaveChanges();
                return question;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool SavePost(int viewerId, int postId)
        {
            try
            {
                Post post = Db.Posts.Find(postId);
                Viewer viewer = Db.Viewers.Find(viewerId);

                post.SavedPosts.Add(viewer);
                Db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public List<Post> GetSavedPosts(int viewerId)
        {
            Viewer viewer = Db.Viewers.Find(viewerId);
            return viewer.SavedPosts.ToList();
        }

        public bool unSavePost(int postId, int viewerId)
        {
            try {
                Post post = Db.Posts.Find(postId);
                Viewer viewer = Db.Viewers.Find(viewerId);

                viewer.SavedPosts.Remove(post);
                Db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}