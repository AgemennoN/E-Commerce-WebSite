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
            TblOrder Order = null;
            TblUser User = (TblUser)Session["User"];
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMessage = "";
            string Redirect = "~/Home";
            // Bu kısmı TempData ile Cart sayfasından gelen verilerin boş olup olmadığı kontrol ettirilecek...
            if (TempData["CartOrders"].ToString() != "" && TempData["CartOrders"] != null)
            {
                if (((decimal)TempData["TotalPrice"]) != 0 && TempData["TotalPrice"] != null)
                {
                    Order = new TblOrder();
                    Order.Name = Request.Form["NameSurname"].ToString();
                    Order.PhoneNumber = Request.Form["PhoneNumber"].ToString();
                    Order.Address = Request.Form["Adress"].ToString();
                    Order.City = Request.Form["City"].ToString();
                    Order.OrderedCarts = (string)TempData["CartOrders"];
                    Order.TotalPrice = (decimal)TempData["TotalPrice"];
                    Order.IsDelivered = false;
                    Order.OrderDateTime = DateTime.Now;
                    Order.UserId = User.UserId;
                }
            }

            if (User != null && Order != null)
            {
                if (BusinessLayer.CartIsEmpty(User.UserId))
                {
                    bool Succes = BusinessLayer.AddPayment(Order, out OMessage);

                    if (Succes == true)
                    {
                        BusinessLayer.isUpdatedCart(User.UserId);

                        TempData["ResultMessage"] =
                            "'Siparişiniz Tamamlandı...\nAna Sayfaya Yönlendirliyorsunuz...'";
                    }
                    else
                    {
                        TempData["ResultMessage"] =
                            "'Siparişiniz Tamamlanamadı...\nSepetinizde Değişiklik olabilir...'";
                        Redirect = "~/Cart/Index";
                    }
                }
            }
            else
            {
                TempData["ResultMessage"] =
                    "'Siparişiniz Tamamlanamadı...\nSepetinizde Değişiklik olabilir...'";
                Redirect = "~/Cart/Index";
            }

            return new RedirectResult(Redirect);
        }

        #region hhuseyin.demirtas (Kullanim disi)
        //hhuseyin.demirtas --- user icin kart tablosunda degisiklik var mı?
        public bool CheckCartUpdates()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            List<TblProduct> ProductInfos = new List<TblProduct>();
            TblUser User = (TblUser)Session["User"];
            bool result = false;

            string BeforeCart = "";
            decimal BeforePrice = 0;
            string OMesage = "";
            if (TempData["CartOrders"] != null && TempData["TotalPrice"] != null)
            {
                BeforeCart = TempData["CartOrders"].ToString();
                BeforePrice = Convert.ToDecimal(TempData["TotalPrice"]);
            }

            if (User != null)
            {
                List<TblCart> CartItems = BusinessLayer.GetCartItems(
                    User.UserId,
                    out ProductInfos,
                    out OMesage
                );

                result = IsChanegd(CartItems, ProductInfos, BeforeCart, BeforePrice);
            }
            return result;
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
