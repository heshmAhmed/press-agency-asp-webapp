using press_agency_asp_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.ViewModels
{
    public class ActorTypeViewModel
    {
        public Actor Actor { get; set; }
        public string UserName { get; set; }
        public ICollection<UserType> UserTypes { get; set; }
    }
}