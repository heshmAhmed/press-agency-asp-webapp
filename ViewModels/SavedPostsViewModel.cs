using press_agency_asp_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.ViewModels
{
    public class SavedPostsViewModel
    {
        public List<InteractionViewModel> Interactions { get; set; }
        public List<Post> SavedPosts { get; set; }
    }
}