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

        // Urun Listeleme Sayfasi
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

            TempData["ProductMessage"] = OMessage;
            return RedirectToAction("ProductList", "Admin");
        }

        // Get: Urun Duzenleme Sayfasi
        [HttpGet]
        public ActionResult ProductEdit()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.CategoryList = BusinessLayer.GetCategoryList(out OMessage);

            if (Request.QueryString["ProductId"] != null)
            {
                int ProductId = new int();
                TblProduct Product = null;
                if (Int32.TryParse(Request.QueryString["ProductId"], out ProductId))
                    // If the parameter is Parseble
                    Product = BusinessLayer.GetProductByProductId(ProductId, out OMessage);

                if (Product == null)
                    // if the item with the ProdectId does not exist Send Admin to the ProductList Page
                    return RedirectToAction("ProductList", "Admin");

                ViewBag.ProductbyId = Product;  // Else sent the product to the ProductEdit Page
            }
            else if (Request.QueryString["ProductId"] == null)
            {   // if Url is entered by hand and the parameter "ProductId" doesn't entered.
                return RedirectToAction("ProductList", "Admin");
            }
            return View(ViewBag);
        }

        // Post: Urun Duzenleme Sayfasi
        [HttpPost]
        public ActionResult ProductEdit(TblProduct Product)
        {
            string ProductId = Request.Form["TxtProductId"].ToString();
            string ProductName = Request.Form["TxtProductName"].ToString();
            string ProductImage;
            if (Request.Form["TxtProductImage"] == null || Request.Form["TxtProductImage"].ToString() == "")
                ProductImage = "/wwwroot/images/1.png";
            else
                ProductImage = Request.Form["TxtProductImage"].ToString();
            string ProductPrice = Request.Form["TxtProductPrice"].ToString();
            string ProductDiscount = Request.Form["TxtProductDiscount"].ToString();
            string ProductStock = Request.Form["TxtProductStock"].ToString();
            string Category = Request.Form["TxtCategory"].ToString();

            Product.ProductId = Convert.ToInt32(ProductId);
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
            BusinessLayer.EditProduct(Product, out OMessage);

            TempData["ProductMessage"] = OMessage;
            return RedirectToAction("ProductList", "Admin");
        }

        // Urun Detay Sayfasi
        public ActionResult ProductDetail(int id)
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.Detail = BusinessLayer.GetProductByProductId(id, out OMessage);
            return View(ViewBag);

        }

        // Urun Silme Sayfasi
        public ActionResult ProductDelete(string Id)
        {
            int ProductId = Convert.ToInt32(Id);
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.ProductDeleteFromDb(ProductId, out OMessage);
            TempData["ProductMessage"] = OMessage;
            return RedirectToAction("ProductList", "Admin");
        }


        // Kategori Listeleme Sayfasi
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


        // Get: Urun Duzenleme Sayfasi
        [HttpGet]
        public ActionResult CategoryEdit()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;

            if (Request.QueryString["CategoryId"] != null)
            {
                int CategoryId = new int();
                TblCategory Category = null;
                if (Int32.TryParse(Request.QueryString["CategoryId"], out CategoryId))
                {
                    // If the parameter is Parseble
                    Category = BusinessLayer.GetCategoryById(CategoryId, out OMessage);
                }
                if (Category == null)
                {
                    // if the item with the CategoryId does not exist Send Admin to the CategoryList Page
                    return RedirectToAction("CategoryList", "Admin");
                }

                ViewBag.CategoryById = Category;  // Else sent the Category to the CategoryEdit Page
            }
            else if (Request.QueryString["CategoryId"] == null)
            {   // if Url is entered by hand and the parameter "CategoryId" doesn't entered.
                return RedirectToAction("CategoryList", "Admin");
            }
            return View(ViewBag);
        }

        // Post: Urun Duzenleme Sayfasi
        [HttpPost]
        public ActionResult CategoryEdit(TblCategory Category)
        {

            string CategoryName = Request.Form["TxtCategoryName"].ToString();
            string CategoryId = Request.Form["TxtCategoryId"].ToString();

            Category.CategoryName = CategoryName;
            Category.CategoryId = Convert.ToInt32(CategoryId);


            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            BusinessLayer.EditCategory(Category, out OMessage);

            ////return new RedirectResult("~/Admin");
            return RedirectToAction("CategoryList", "Admin");
        }

        // Kategori Silme Sayfasi
        public ActionResult CategoryDelete(string Id)
        {
            int CategoryId = Convert.ToInt32(Id);
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.CategoryDeleteFromDb(CategoryId, out OMessage);
            return RedirectToAction("CategoryList", "Admin");
        }

        // Kullanici Listeleme Sayfasi
        public ActionResult UserList()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            ViewBag.GetListOfUsers = BusinessLayer.GetUserList(out OMessage);
            ViewBag.GetListOfSubscribers = BusinessLayer.GetSubscriberList(out OMessage);
            return View(ViewBag);
        }

        // Kullanici Silme Sayfasi
        public ActionResult UserDelete(string Id)
        {
            int UserId = Convert.ToInt32(Id);
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.UserDeleteFromDb(UserId, out OMessage);
            TempData["UserRemoved"] = OMessage;
            return RedirectToAction("UserList", "Admin");
        }

        // Abone Silme Sayfasi
        public ActionResult SubscriberDelete(string MailAddress)
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.RemoveSubscriber(MailAddress, out OMessage);
            TempData["UserRemoved"] = OMessage;
            return RedirectToAction("UserList", "Admin");
        }

        // Siparis Dagitim Sayfasi
        public ActionResult OrderDeliver(string Id)
        {
            int OrderId = Convert.ToInt32(Id);
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.OrderDeliverById(OrderId, out OMessage);
            return RedirectToAction("Index", "Admin");
        }



        public ActionResult OrderDetail()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;

            if (Request.QueryString["OrderId"] != null)
            {
                int OrderId = new int();
                TblOrder Order = null;
                List<TblCart> CartsInOrder = new List<TblCart>();
                if (Int32.TryParse(Request.QueryString["OrderId"], out OrderId))
                {   // If the parameter is Parseble

                    Order = BusinessLayer.GetOrderByOrderId(OrderId, out OMessage);
                    CartsInOrder = BusinessLayer.GetCartsInsideTheOrder(Order);
                }
                if (Order == null)
                    // if the item with the OrderId does not exist Sent Admin to the OrderList Page
                    return RedirectToAction("Index", "Admin");

                // Else sent the Order and Cartlist to the OrderDetail Page
                ViewBag.CartsInOrder = CartsInOrder;
                ViewBag.OrderbyId = Order;  
            }
            else if (Request.QueryString["OrderId"] == null)
            {   // if Url is entered by hand and the parameter "OrderId" doesn't entered.
                return RedirectToAction("Index", "Admin");
            }
            return View(ViewBag);
        }


        public ActionResult UserDetail()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            if (Request.QueryString["UserId"] != null)
            {
                int UserId = new int();
                TblUser User = null;
                if (Int32.TryParse(Request.QueryString["UserId"], out UserId))
                {   // If the parameter is Parseble
                    User = BusinessLayer.GetUserByUserId(UserId);
                    ViewBag.AllOrders = BusinessLayer.GetOrderList(out OMessage);
                }
                if (User == null)
                    // if the item with the UserId does not exist Sent Admin to the UserList Page
                    return RedirectToAction("UserList", "Admin");

                // Else sent the User to the UserDetail Page
                ViewBag.UserById = User;
            }
            else if (Request.QueryString["UserId"] == null)
            {   // if Url is entered by hand and the parameter "UserId" doesn't entered.
                return RedirectToAction("UserList", "Admin");
            }
            return View(ViewBag);
        }

        public ActionResult ContactList()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            
            List<TblContact> Messages = BusinessLayer.GetContacts(out string OMessage);
            ViewBag.ContactMessages = Messages;
            return View(ViewBag);
        }

        // Abone Silme Sayfasi
        public ActionResult ContactMessageDelete(int MessageId)
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage;
            bool Success = BusinessLayer.ContactMessageDelete(MessageId, out OMessage);
            TempData["ContactMessageDelete"] = OMessage;
            return RedirectToAction("ContactList", "Admin");
        }

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - End
        //Admin Sayfasi Urunler Listesi Yapildi
    }
}