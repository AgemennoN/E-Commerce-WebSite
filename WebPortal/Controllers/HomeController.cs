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
            TblCategory Category = business.GetCategoryById(id, out OMessage);
            if (Category != null)
            {
                ViewBag.Category = Category;
                ViewBag.ProductsByCategory = business.GetProductsByCategoryId(Category.CategoryId, out OMessage);
            }
            else if (id != null && Category == null)
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

        #region Celal Serdar Ergun Aksiyon
        // Celal Serdar Ergun Aksiyon
        // Ekleme aksiyonunu yapar
        public ActionResult AddProductAction()
        {
            string OMessage = "Sepete Eklemek Icin Lutfen Uye Girisi Yapiniz";
            string ProductIdString = Request.Form["ProductId"].ToString();
            string ReturnUrl = Request.Form["ReturnUrl"].ToString();
            int ProductId = Convert.ToInt32(ProductIdString);
            TblUser User = (TblUser)Session["User"];

            //TblUser User = new TblUser(); // test icin
            //  User.UserId = 2; // test icin
            bool IsAdded = false;

            try
            {
                if (User != null)
                {
                    try
                    {
                        TBusinessLayer BusinessLayer = new TBusinessLayer();
                        IsAdded = BusinessLayer.AddProductToCart(
                            ProductId,
                            User.UserId,
                            out OMessage
                        );
                    }
                    catch (Exception ex)
                    {
                        OMessage = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }

            TempData["CartMessage"] = OMessage;

            if (ReturnUrl != null)
                return new RedirectResult(ReturnUrl);
            else
                return new RedirectResult("~/");
        }
        // Celal Serdar Ergun - End
        #endregion
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


