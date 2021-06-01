using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Security
{
    public static class SessionPersister 
    {
        static string identitySessinvar = "identity";
        public static int userId {
            get
            {
                if (HttpContext.Current == null)
                    return 0;
                var sessionVar = HttpContext.Current.Session["userId"];
                if (sessionVar != null)
                    return (int) sessionVar;
                return 0;
            }
            set
            {
                HttpContext.Current.Session["userId"] = value;
            }
        }
        public static string userType {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session["userType"];
                if (sessionVar != null)
                    return sessionVar as string;
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["userType"] = value;
            }
        }
        public static string userFullName
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session["userFullName"];
                if (sessionVar != null)
                    return sessionVar as string;
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["userFullName"] = value;
            }
        }
        public static string Identity
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[identitySessinvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[identitySessinvar] = value;
            }
        }
    }
}