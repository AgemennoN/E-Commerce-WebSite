using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace WebPortal.Controllers
{
    public class ProductsController : Controller
    {
        TBusinessLayer BusinessLayer = new TBusinessLayer();
        // GET: Products
        public ActionResult Index()
        {
            string OMessage;
            


            return View();            
        }
    }
}