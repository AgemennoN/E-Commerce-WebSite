using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using PortalDataLayer;


namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        //>>>Hüseyin Bilgiç<<<
        public ActionResult Index(string id)
        {
            TBusinessLayer business = new TBusinessLayer();
            string OMessage;
            TblCategory Category= business.GetCategoryById(id, out OMessage);
            if (Category!=null)
            {
                ViewBag.Category = Category;
                ViewBag.ProductsByCategory = business.GetProductsByCategoryId(Category.CategoryId, out OMessage);
            }
            else if(id !=null&& Category==null)
            {
                return new RedirectResult("/");
            }
            ViewBag.Categories = business.GetCategories(out OMessage);

            //>>>Belgin Çoban--<<<
            ViewBag.ProductsOnSales = business.GetProducDiscounts(out OMessage);
            //>> --End<<<

            return View(ViewBag);
        }
        //>>>Hüseyin Bilgiç<<<

        //<<<Buket Soyhan - START
        public ActionResult AddSubscriber()
        {
            string OMessage;
            bool Success = false;

            string MailAddress = Request.Form["Email"].ToString();
            string RequestButton = Request.Form["Input"].ToString();

            try
            {
                TBusinessLayer BusinessLayer = new TBusinessLayer();
                bool IsSubs = BusinessLayer.IsSubscriber(MailAddress, out OMessage);
                if (RequestButton == "Abone Ol") 
                {
                    if (IsSubs)         
                    {
                        OMessage = MailAddress + " adresi zaten kayıt edilmiş.";
                        Success = false;
                    }
                    else if (!IsSubs)
                        Success = BusinessLayer.AddSubscriber(MailAddress, out OMessage);
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            TempData["Subscriber"] = OMessage;

            return new RedirectResult("~/Home");
        }
        //Buket Soyhan - END>>>
    }
}