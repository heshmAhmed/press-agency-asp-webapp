using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Models
{
    public class Viewer : Actor 
    {
        public virtual ICollection<Interaction> Interactions { get; set; }
        public virtual ICollection<Post> SavedPosts { get; set; }
        public virtual ICollection<Question> Questions { get; set; }


        public Viewer()
        {
            this.SavedPosts = new HashSet<Post>();
            this.Interactions = new HashSet<Interaction>();
            this.Questions = new HashSet<Question>();
        }
    }
}