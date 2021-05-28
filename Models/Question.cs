using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int ViewerId { get; set; }
        public virtual Viewer Viewer { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string Answer { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        

    }   
}