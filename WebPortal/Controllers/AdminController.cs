using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalDataLayer;

namespace WebPortal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        //Admin Sayfasi Urunler Listesi Yapildi
        public ActionResult Index(string Id)
        {
            if (Id != "All")
            {
                TBusinessLayer BusinessLayer = new TBusinessLayer();
                string OMessage;
                ViewBag.GetList = BusinessLayer.GetNonDeliveredOrderList(out OMessage);
                return View(ViewBag);
            }
            else
            {
                TBusinessLayer BusinessLayer = new TBusinessLayer();
                string OMessage;
                ViewBag.GetList = BusinessLayer.GetOrderList(out OMessage);
                return View(ViewBag);
            }

        }
        public ActionResult ProductList()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.GetList = BusinessLayer.GetProductList(out OMessage);
            return View(ViewBag);
        }

        // Get: Urun Ekleme Sayfasi
        [HttpGet]
        public ActionResult ProductCreate()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.CategoryList = BusinessLayer.GetCategoryList(out OMessage);

            return View(ViewBag);
        }

        // Post : Urun Ekleme Sayfasi
        [HttpPost]
        public ActionResult ProductCreate(TblProduct Product)
        {
            string ProductName = Request.Form["TxtProductName"].ToString();
            string ProductImage = Request.Form["TxtProductImage"].ToString();
            string ProductPrice = Request.Form["TxtProductPrice"].ToString();
            string ProductDiscount = Request.Form["TxtProductDiscount"].ToString();
            string ProductStock = Request.Form["TxtProductStock"].ToString();
            string Category = Request.Form["TxtCategory"].ToString();

            Product.ProductName = ProductName;
            Product.ProductImage = ProductImage;
            Product.ProductPrice = Convert.ToDecimal(ProductPrice);
            Product.ProductDiscount = Convert.ToDecimal(ProductDiscount);
            // İndirim oranına göre güncel fiyat otomatik olarak eklendi.
            Product.PriceOnSale = Convert.ToDecimal(ProductPrice) * (100 - Convert.ToDecimal(ProductDiscount)) / 100;
            Product.ProductStock = Convert.ToInt32(ProductStock);
            Product.ProductActive = true;
            Product.CategoryId = Convert.ToInt32(Category);

            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            BusinessLayer.AddProduct(out OMessage, Product);

            //return new RedirectResult("~/Admin");
            return RedirectToAction("ProductList", "Admin");
        }

        public ActionResult CategoryList()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.GetList = BusinessLayer.GetCategories(out OMessage);
            return View(ViewBag);
        }

        // Get: Kategori Ekleme Sayfasi
        [HttpGet]
        public ActionResult CategoryCreate()
        {
            return View();
        }

        // Post: Kategori Ekleme Sayfasi
        [HttpPost]
        public ActionResult CategoryCreate(TblCategory Category)
        {
            string CategoryName = Request.Form["TxtCategoryName"].ToString();

            Category.CategoryName = CategoryName;
            Category.CategoryActive = true;

            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            BusinessLayer.AddCategory(out OMessage, Category);

            return RedirectToAction("CategoryList", "Admin");
        }

        public ActionResult UserList()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.GetListByUsers = BusinessLayer.GetUserList(out OMessage);
            ViewBag.GetListBySubscribers = BusinessLayer.GetSubscriberList(out OMessage);
            return View(ViewBag);
        }

        public ActionResult ProductDelete(string Id)
        {
            int ProductId = Convert.ToInt32(Id);
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.ProductDeleteFromDb(ProductId, out OMessage);
            return RedirectToAction("ProductList", "Admin");
        }

        public ActionResult CategoryDelete(string Id)
        {
            int CategoryId = Convert.ToInt32(Id);
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.CategoryDeleteFromDb(CategoryId, out OMessage);
            return RedirectToAction("CategoryList", "Admin");
        }

        public ActionResult UserDelete(string Id)
        {
            int UserId = Convert.ToInt32(Id);
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.UserDeleteFromDb(UserId, out OMessage);
            return RedirectToAction("UserList", "Admin");
        }

        public ActionResult SubsriberDelete(string Id)
        {
            int SubsriberId = Convert.ToInt32(Id);
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.SubscriberDeleteFromDb(SubsriberId, out OMessage);
            return RedirectToAction("UserList", "Admin");
        }

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - End
        //Admin Sayfasi Urunler Listesi Yapildi
    }
}