using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Util
{
    public abstract class Mapping
    {
        public static Admin MapToAdmin(UserViewModel user)
        {
            return new Admin {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email.ToLower(),
                Password = user.Password,
                Phone = user.Phone,
                UserTypeId = user.UserTypeId,
                CreateDate = DateTime.Now
            };
        }


        public static Editor MapToEditor(UserViewModel user)
        {
            return new Editor
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email.ToLower(),
                Password = user.Password,
                Phone = user.Phone,
                UserTypeId = user.UserTypeId,
                Username = user.UserName,
                CreateDate = DateTime.Now
            };
        }


        public static Viewer MapToViewer(UserViewModel user)
        {
            return new Viewer
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email.ToLower(),
                Password = user.Password,
                Phone = user.Phone,
                UserTypeId = user.UserTypeId,
                CreateDate = DateTime.Now
            };
        }

        public static UserViewModel MapToUserViewModel(Actor actor)
        {
            return new UserViewModel
            {
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Email = actor.Email,
                Phone = actor.Phone,
                UserTypeId = actor.UserTypeId,
                UserTypeName = actor.UserType.Name,
                UserName = actor.UserType.Name == "editor" ? ((Editor) actor).Username:""
            };
        }
    }
}