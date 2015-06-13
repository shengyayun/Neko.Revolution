using System;
using System.Web;
using Library.Processor;

namespace Service
{
    public class Global : HttpApplication
    {
        public static Core Core;

        protected void Application_Start(object sender, EventArgs e)
        {
            Core = new Core(HttpContext.Current.Server.MapPath("/Neko/Settings.xml"), HttpContext.Current.Server.MapPath("/Neko/LogInfo"));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}