using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Models
{
    public class Role
    {
        public string User_role { get; }

        private Role(string role) { User_role = role; }
        public static Role Admin { get { return new Role("admin"); } }
        public static Role Editor { get { return new Role("editor"); } }
        public static Role Viewer { get { return new Role("viewer"); } }


    }
}