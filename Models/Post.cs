using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace press_agency_asp_webapp.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public int No_views { get; set; }
        public int No_likes { get; set; }
        public int No_dislikes { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool? State { get; set; }
        public virtual PostType PostType { get; set; }
        [Required]
        public int PostTypeId  { get; set; }
        public int EditorId { get; set; }
        public virtual Editor Editor { get; set; }
        public virtual ICollection<Interaction> Interactions { get; set; }
        public virtual ICollection<Viewer> SavedPosts { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public string ImagePath { get; set; }

        public Post()
        {
            this.SavedPosts = new HashSet<Viewer>();
            this.Questions = new HashSet<Question>();
            this.Interactions = new HashSet<Interaction>();
        }


    }
}