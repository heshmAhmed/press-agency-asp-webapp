using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public DateTime Create_date { get; set; }
        [Required]
        public bool State { get; set; }
        [Required]
        public string Type { get; set; }
        public Editor Editor { get; set; }
      
    }
}