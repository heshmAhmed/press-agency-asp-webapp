using press_agency_asp_webapp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace press_agency_asp_webapp.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private ActorService ActorService = ActorService.CreateActorService(new Models.CodeFContext());
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(SessionPersister.Identity))
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    controller = "Actor",
                    action = "LogIn"
                }));
            }
            else
            {
                CustomPrincipal cp = new CustomPrincipal();
                if (!cp.IsInRole(Roles))
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                    {
                        controller = "AccessDenied",
                        action = "Index"
                    }));
                }
            }
        }
    }
}