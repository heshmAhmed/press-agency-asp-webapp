using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Models
{
    public class PostType
    {
        public string Type { get; set; }
        private PostType(string type) { Type = type; }
        public static PostType Sports { get { return new PostType("sports"); } }
        public static PostType Politics { get { return new PostType("politics"); } }
        public static PostType Cinema { get { return new PostType("cinema"); } }


    }
}