using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.Util;
using press_agency_asp_webapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web.Helpers;

namespace press_agency_asp_webapp.Services
{
    public class ActorService
    {
        CodeFContext Db;
        private static ActorService Instanse = null;
        private ActorService(CodeFContext Db) { this.Db = Db; }

        public static ActorService CreateActorService(CodeFContext codeFContext)
        {
            if (Instanse == null)
                Instanse = new ActorService(codeFContext);
            return Instanse;
        }
        
        public void CreateActor(UserViewModel userViewModel)
        {
            UserType userType = GetUserType(userViewModel.UserTypeId);
                        
            switch (userType.Name)
            {
                case "admin":
                    Db.Admins.Add(Mapping.MapToAdmin(userViewModel));
                    break;
                case "editor":
                    Db.Editors.Add(Mapping.MapToEditor(userViewModel));
                    break;
                case "viewer":
                    Db.Viewers.Add(Mapping.MapToViewer(userViewModel));

                    break;
            }
            Db.SaveChanges();

        }

        private UserType GetUserType(int userTypeId)
        {
            return Db.UserTypes.Where(UserType => UserType.Id == userTypeId).FirstOrDefault();
        }

        public Actor FindActor(string identity)
        {
            return GeneralUtil.ISEmail(identity) ? FindActorByEmail(identity) : FindActorByEmail(identity);
        }

        public Actor FindActor(string identity, string password)
        {
            if (GeneralUtil.ISEmail(identity))
            {
                return Db.Actors.Where(actor => actor.Email.Equals(identity) && actor.Password.Equals(password)).FirstOrDefault();
            }
            return Db.Editors.Where(editor => editor.Username.Equals(identity) && editor.Password.Equals(password)).FirstOrDefault();
        }

        public Actor FindActor(int actorId)
        {
            return Db.Actors.Where(actor => actor.Id == actorId).FirstOrDefault();
        }

        private Editor FindEditor(IdentityViewModel identityViewModel)
        { 
            Editor editor = FindActorByUsername(identityViewModel.Identity);
            return editor != null && editor.Password == identityViewModel.Password ? editor : null ;
        }

        public Editor FindEditor(int id)
        {
            return Db.Editors.Where(editor => editor.Id == id).FirstOrDefault();
        }

        private Actor FindActorByEmail(string email)
        {
            return Db.Actors.Where(actor => actor.Email == email).FirstOrDefault();
        }

        private Editor FindActorByUsername(string username)
        {
            return Db.Editors.Where(editor => editor.Username == username).FirstOrDefault();

        }

        public bool CheckEmail(string email)
        {
            return Db.Actors.Where(actor => actor.Email == email).FirstOrDefault() != null;
        }

        public bool CheckUserName(string username)
        {
            return Db.Editors.Where(editor => editor.Username == username).FirstOrDefault() != null;
        }

        public ICollection<Post> GetPosts()
        {
            return Db.Posts.ToList();
        }

        public UserViewModel UpdateAcctor(int id, UserViewModel userViewModel)
        {
            Actor actor = FindActor(id);
            actor.FirstName = userViewModel.FirstName;
            actor.LastName = userViewModel.LastName;
            actor.Phone = userViewModel.Phone;
            if (userViewModel.Password != null && userViewModel.Password.Length > 0)
                actor.Password = userViewModel.Password;
            Db.SaveChanges();
            return Mapping.MapToUserViewModel(actor);
        }


    }
}