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
        public ActionResult Index()
        {
            //TBusinessLayer BusinessLayer = new TBusinessLayer();
            //string OMessage;
            //ViewBag.GetList = BusinessLayer.GetProductList(out OMessage);
            //return View(ViewBag);
            return View();
        }
        public ActionResult ProductList()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.GetList = BusinessLayer.GetProductList(out OMessage);
            return View(ViewBag);
        }

        public ActionResult CategoryList()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.GetList = BusinessLayer.GetCategories(out OMessage);
            return View(ViewBag);
        }
        public ActionResult UserList()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.GetList = BusinessLayer.GetUserList(out OMessage);
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
            Product.ProductStock = Convert.ToInt32(ProductStock);
            Product.ProductActive = true;
            Product.CategoryId = Convert.ToInt32(Category);

            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool IsAded = BusinessLayer.AddProduct(out OMessage, Product);

            //return new RedirectResult("~/Admin");
            return RedirectToAction("ProductList", "Admin");
        }

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - End
        //Admin Sayfasi Urunler Listesi Yapildi
    }
}