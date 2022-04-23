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
    } //paymet.html dosyasında inputlardan value değerlerini 22-26 satirlari arasinda aliyoruz. Daha sonra Daha sonra session var mi diye bakiyoruz. Giris yaptiysa session baslamıs oluyor ve bize useri donduruyor.
} //Verileri saglikli bir sekilde ekleyip ekleyemedigimizi bool succes degiskeniyle kontrol ediyoruz. eger true ise isupdatedcart a userid gonderiyor.
//Emre Tuzunoglu End
