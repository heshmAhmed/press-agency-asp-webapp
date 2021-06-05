using press_agency_asp_webapp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Services
{
    public class EditorService
    {
        CodeFContext Db;
        private static EditorService Instanse = null;
        private EditorService(CodeFContext Db) { this.Db = Db; }

        public static EditorService CreateEditorService(CodeFContext codeFContext)
        {
            if (Instanse == null)
                Instanse = new EditorService(codeFContext);
            return Instanse;
        }

        public ICollection<Post> GetUnAnsweredPosts(int editorId)
        {
            List<Question> questions = Db.Questions.Where(question => question.Post.EditorId == editorId && question.Answer == null).ToList();
            ISet<Post> posts = new HashSet<Post>();
            foreach(Question question in questions)
            {
                posts.Add(question.Post);
            }
            return posts;
        }

        public bool UpdateAnswer(int questionId, string answer) {
            try
            {
                Question question = Db.Questions.Find(questionId);
                question.Answer = answer;
                Db.Entry(question).State = EntityState.Modified;
                Db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        
        }

        public bool CreatePost(Post post)
        {
            try
            {
                Db.Posts.Add(post);
                Db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public List<PostType> GetPostTypes()
        {
            return Db.PostTypes.ToList();
        }

        public List<Post> GetPosts(int editorId)
        {
            return Db.Posts.Where(post => post.EditorId == editorId).ToList();
        }

        public void DeletePost(int postId, int editorId)
        {
            Post post = Db.Posts.Where(row => row.Id == postId).FirstOrDefault();
            Db.Posts.Remove(post);
            Db.SaveChanges();
        }

        public Post GetPost(int postId)
        {
            return Db.Posts.Find(postId);
        }

        public bool UpdatePost(Post post)
        {
            try
            {
                Post post1 = Db.Posts.Find(post.Id);
                post1.Title = post.Title;
                post1.PostTypeId = post.PostTypeId;
                post1.Body = post.Body;
                post1.State = null;
                post1.ImagePath = post.ImagePath == null ? post1.ImagePath : post.ImagePath;
                Db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }


    }
}