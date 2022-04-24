using BusinessLayer;
using PortalDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        // Emre Tuzunoglu Begin
        public ActionResult AddPayment()
        {
            TblOrder Order = new TblOrder();
            TblUser User = (TblUser)Session["User"];
            string OMessage = "";
            Order.Name = Request.Form["NameSurname"].ToString();
            Order.PhoneNumber = Request.Form["PhoneNumber"].ToString();
            Order.Address = Request.Form["Adress"].ToString();
            Order.City = Request.Form["City"].ToString();
            Order.OrderedCarts = (string)TempData["CartOrders"];
            Order.TotalPrice = (decimal)TempData["TotalPrice"];
            Order.IsDelivered = false;
            Order.OrderDateTime = DateTime.Now;

            Order.UserId = User.UserId;
            if (User != null)
            {
                TBusinessLayer BusinessLayer = new TBusinessLayer();
                bool Succes = BusinessLayer.AddPayment(Order, out OMessage);

                if (Succes == true)
                {
                    BusinessLayer.isUpdatedCart(User.UserId);
                }
            }
            return new RedirectResult("~/Home");
        }

        #region hhuseyin.demirtas
        //hhuseyin.demirtas --- user icin kart tablosunda degisiklik var mı?
        public ActionResult CheckCartUpdates()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            List<TblProduct> ProductInfos = new List<TblProduct>();
            TblUser User = (TblUser)Session["User"];

            string OMesage = "";
            string BeforeCart = TempData["CartOrders"].ToString();
            decimal BeforePrice = Convert.ToDecimal(TempData["TotalPrice"]);

            if (User != null)
            {
                List<TblCart> CartItems = BusinessLayer.GetCartItems(
                    User.UserId,
                    out ProductInfos,
                    out OMesage
                );

                ViewBag.IsChanged = IsChanegd(CartItems, ProductInfos, BeforeCart, BeforePrice);
            }
            return View(ViewBag);
        }

        //hhuseyin.demirtas ---TempData ve guncel cart tablosu arasında kullanıcı için farklılık var mı?
        public bool IsChanegd(
            List<TblCart> CartItems,
            List<TblProduct> ProductInfos,
            string BeforeCart,
            decimal BeforePrice
        )
        {
            bool result = false;
            decimal LatestTotalPrice = 0;
            string LatestOrders = "";
            for (int i = 0; i < CartItems.Count; i++)
            {
                decimal Discount = 0;
                Discount = (decimal)(
                    ProductInfos[i].ProductPrice * (ProductInfos[i].ProductDiscount / 100)
                );
                decimal LastPrice = (decimal)(ProductInfos[i].ProductPrice - Discount);

                LatestTotalPrice += (decimal)(CartItems[i].Quantity * LastPrice);
                LatestOrders += CartItems[i].ProductId.ToString() + ",";
            }

            if (BeforeCart != LatestOrders && BeforePrice != LatestTotalPrice)
                result = false;
            else
            {
                result = true;
            }

            return result;
        }
        #endregion
    } //paymet.html dosyasında inputlardan value değerlerini 22-26 satirlari arasinda aliyoruz. Daha sonra Daha sonra session var mi diye bakiyoruz. Giris yaptiysa session baslamıs oluyor ve bize useri donduruyor.
} //Verileri saglikli bir sekilde ekleyip ekleyemedigimizi bool succes degiskeniyle kontrol ediyoruz. eger true ise isupdatedcart a userid gonderiyor.
//Emre Tuzunoglu End
