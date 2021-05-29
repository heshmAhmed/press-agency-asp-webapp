using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;

namespace press_agency_asp_webapp.Util
{
    public abstract class GeneralUtil
    {
        public static bool ISEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }


        public static bool CheckPassword(string hashedPass, string unHashedPass)
        {
            Debug.WriteLine(unHashedPass);
            return Crypto.HashPassword(unHashedPass) == hashedPass;

        }

    }
}