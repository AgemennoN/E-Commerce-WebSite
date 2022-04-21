using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessLayer;

namespace WebPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string OMessage;
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Application["Products"] = BusinessLayer.GetProducts(out OMessage);
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
