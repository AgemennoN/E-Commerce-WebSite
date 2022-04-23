using BusinessLayer;
using PortalDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Controllers
{
    public class CampaignsController : Controller
    {
       
        //>>>Belgin Çoban -- Start<<<
        public ActionResult Index()
        {
            TBusinessLayer business = new TBusinessLayer();
            string OMessage;

            ViewBag.Products = business.GetProducDiscounts(out OMessage);
            return View(ViewBag);
        }
        //>>>Belgin Çoban --End<<<
    }
}