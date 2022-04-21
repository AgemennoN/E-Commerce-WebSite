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
            
            var Sorting = Request.Form["Sort"];
            string[] sorting = Request.Form.GetValues("IdSort");

            if(Request.QueryString["UrunAdi"]!=null)
            {
                string UrunAdi = Request.QueryString["UrunAdi"].ToString();
                ViewBag.Products = BusinessLayer.GetFoundProducts(out OMessage,UrunAdi);
            }
            else if(Sorting == "ascending,")
            {
                ViewBag.Products = BusinessLayer.GetProducts(out OMessage);
            }
            else if (Sorting == "descending,")
            {
                ViewBag.Products = BusinessLayer.GetProducts(out OMessage);
            }
            else
            {
                ViewBag.Products = BusinessLayer.GetProducts(out OMessage);
            }

            return View();            
        }
    }
}