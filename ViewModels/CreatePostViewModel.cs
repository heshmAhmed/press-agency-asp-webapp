using press_agency_asp_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.ViewModels
{
    public class CreatePostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int No_Views { get; set; }
        public int No_DisLikes { get; set; }
        public int No_Likes { get; set; }
        public int PostTypeId { get; set; }
        public ICollection<PostType> PostTypes { get; set; }
        public HttpPostedFileBase PostImage { get; set; }
    }
}