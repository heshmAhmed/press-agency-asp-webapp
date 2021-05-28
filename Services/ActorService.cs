using press_agency_asp_webapp.Models;
using press_agency_asp_webapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
        public bool CreateActor(ActorTypeViewModel ActorTypeViewModel)
        {
            return false;
        }

        public bool CheckEmail(string email)
        {
            return Db.Actors.Where(actor => actor.Email == email).FirstOrDefault() != null;
        }

        public bool CheckUserName(string username)
        {
            return Db.Editors.Where(editor => editor.Username == username).FirstOrDefault() != null;
        }
    }
}