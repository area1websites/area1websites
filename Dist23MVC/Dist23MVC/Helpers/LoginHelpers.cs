using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dist23MVC.Helpers
{
    public static class LoginHelpers
    {
        public static bool isLoggedIn()
        {
            if (HttpContext.Current.Session["LoginName"] == null) 
            {
                return false;
            }
            return true;
        }
    }
}