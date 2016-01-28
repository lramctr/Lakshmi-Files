using System;
using System.IO;
using System.Web;
using Nps.Gis.PivotServerTools;

namespace PivotServer
{
    public class Global : System.Web.HttpApplication
    {
        public Global()
        {
        }

        public string AssemblyFolder
        {
            get { return Path.Combine(HttpRuntime.AppDomainAppPath, "bin"); }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
            PivotHttpHandlers.ApplicationEnd();
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
    }
}
