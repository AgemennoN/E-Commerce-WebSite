using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            Application["Categories"] = BusinessLayer.GetCategories(out OMessage);
        }

        void Session_Start()
        {
            Session["Admin"] = false;
        }

        //void Session_End()
        //{
        //}

    }
}
