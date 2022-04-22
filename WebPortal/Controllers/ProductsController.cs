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
        //<<< Vejdi BURAK - Start
        TBusinessLayer BusinessLayer = new TBusinessLayer();
        // GET: Products
        public ActionResult Index()
        {            
            string OMessage;
            var SortingType = Request.Form["Sort"];//form checkboxtan gelen secenek            

            if(Request.QueryString["UrunAdi"]!=null)//Urun arama ile donen urunler listesi
            {
                string UrunAdi = Request.QueryString["UrunAdi"].ToString().ToLower();
                ViewBag.Products = BusinessLayer.GetFoundProducts(out OMessage,UrunAdi);
            }
            else if(SortingType == "ascending,")//en dusuk fiyata gore sıranan urunler listesi
            {
                ViewBag.Products = BusinessLayer.GetProducts(out OMessage);
            }
            else if (SortingType == "descending,")//en yuksek fiyata gore sıralanan urunler listesi
            {
                ViewBag.Products = BusinessLayer.GetProducts(out OMessage);
            }
            else // normal sekilde goruntulenecek urunler listesi
            {
                ViewBag.Products = BusinessLayer.GetProducts(out OMessage);
            }
            return View();            
        }
        // Vejdi BURAK - End
    }
}