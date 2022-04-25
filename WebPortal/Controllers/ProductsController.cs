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
        string UrunAdi;
        //<<< Vejdi BURAK - Start
        TBusinessLayer BusinessLayer = new TBusinessLayer();
        // GET: Products
        public ActionResult Index()
        {
            string OMessage;
            var SortingType = Request.Form["Sort"];//form checkboxtan gelen secenek            

            if (Request.QueryString["UrunAdi"] != null)//Urun arama ile donen urunler listesi
            {
                UrunAdi = Request.QueryString["UrunAdi"].ToString().ToLower();
                ViewBag.Products = BusinessLayer.GetFoundProducts(out OMessage, UrunAdi);
            }
            else // normal sekilde goruntulenecek urunler listesi
            {
                ViewBag.Products = BusinessLayer.GetProducts(out OMessage);
            }
            return View();
        }
        // Vejdi BURAK - End

        //FIRAT --START
        public ActionResult LowPrice()
        {
            string OMessage;
            if (TempData["UrunAdi"] != null)
            {
                string UrunAdi = TempData["UrunAdi"].ToString();
                ViewBag.UrunAdi = UrunAdi;
                ViewBag.LowProductsList = BusinessLayer.GetFoundLowProducts(out OMessage,UrunAdi);
            }
            else
            {                
                ViewBag.LowProductsList = BusinessLayer.GetLowPrice(out OMessage);                
            }
            return View();
        }
        public ActionResult HighPrice()
        {
            string OMessage;
            if (TempData["UrunAdi"] != null)
            {
                string UrunAdi = TempData["UrunAdi"].ToString();
                ViewBag.UrunAdi = UrunAdi;
                ViewBag.HighProductsList = BusinessLayer.GetFoundHighProducts(out OMessage, UrunAdi);
            }
            else
            {
                ViewBag.HighProductsList = BusinessLayer.GetHighPrice(out OMessage);
            }
            return View();
        }
        

        //fırat--END

    }
}