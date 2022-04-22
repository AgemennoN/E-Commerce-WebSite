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

        //FIRAT --START
        public ActionResult LowPrice()
        {
            string OMessage;
            ViewBag.LowProductsList = BusinessLayer.GetLowPrice(out OMessage);
            return View();
        }
        public ActionResult HighPrice()
        {
            string OMessage;
            ViewBag.HighProductsList = BusinessLayer.GetHighPrice(out OMessage);
            return View();
        }
        //fırat--END


    }
}