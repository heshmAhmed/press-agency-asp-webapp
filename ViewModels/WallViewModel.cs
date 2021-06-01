using press_agency_asp_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.ViewModels
{
    public class WallViewModel
    {
        public ICollection<Post> Posts { get; set; }
        
        public ICollection<InteractionViewModel> Interactions { get; set; }
    }
}