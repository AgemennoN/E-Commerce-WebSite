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
        public ActionResult Index(string id)
        {
            TBusinessLayer business = new TBusinessLayer();
            string OMessage;
            TblCategory Category = business.GetCategoryById(id, out OMessage);
            if (Category != null)
            {
                ViewBag.Category = Category;
                ViewBag.Products = business.GetProductsByCategoryId(
                    Category.CategoryId,
                    out OMessage
                );
            }
            else if (id != null && Category == null)
            {
                return new RedirectResult("/");
            }
            ViewBag.Categories = business.GetCategories(out OMessage);
            return View(ViewBag);
        }

        #region Celal Serdar Ergun Aksiyon
        // Celal Serdar Ergun Aksiyon
        // Ekleme aksiyonunu yapar
        public ActionResult AddProductAction()
        {
            string OMessage = "Urun sepete eklenemedi";
            ;
            string ProductIdString = Request.Form["ProductId"].ToString();
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

            return new RedirectResult("~/");
        }
        // Celal Serdar Ergun - End
        #endregion


    }
}
