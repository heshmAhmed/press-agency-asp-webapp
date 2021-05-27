using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Models
{
    public class Viewer : Actor 
    {
        public ICollection<Interaction> Interactions { get; set; }
    }
}