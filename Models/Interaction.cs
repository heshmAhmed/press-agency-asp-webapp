using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Models
{
    public class Interaction
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int ViewerId { get; set; }
        public Viewer Viewer { get; set; }
        public bool IsLike { get; set; }

    }
}