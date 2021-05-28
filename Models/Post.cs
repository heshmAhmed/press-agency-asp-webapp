using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace press_agency_asp_webapp.Models
{
    public class Post
    {
        public int Id { get; set; }
        //public Actor Editor { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public int No_views { get; set; }
        public int No_likes { get; set; }
        public int No_dislikes { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public bool State { get; set; }
        [Required]
        public PostType PostType { get; set; }
        public int PostTypeId  { get; set; }
        public int EditorId { get; set; }
        public Editor Editor { get; set; }
        public virtual ICollection<Interaction> Interactions { get; set; }
        public virtual ICollection<Viewer> ViewerPosts { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

        public Post()
        {
            this.ViewerPosts = new HashSet<Viewer>();
        }


    }
}