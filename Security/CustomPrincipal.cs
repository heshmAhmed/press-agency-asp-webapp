using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.Services;
using press_agency_asp_webapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace press_agency_asp_webapp.Security
{
    public class CustomPrincipal : IPrincipal
    { 
       public IIdentity Identity { get; set; }

        public CustomPrincipal()
        {
            this.Identity = new GenericIdentity(SessionPersister.Identity);
        }
        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            return roles.Contains(SessionPersister.userType);    
        }
    }
}