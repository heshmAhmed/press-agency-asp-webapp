using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.ViewModels
{
    public class InteractionViewModel
    {
        public int PostID { get; set; }
        public int ViewerID { get; set; }
        public bool IsLike { get; set; }
    }
}