using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using PortalDataLayer;

namespace WebPortal.Controllers
{
    // hhuseyin.demirtas
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            List<TblProduct> ProductInfos = new List<TblProduct>();
            string OMesage = "";
            TblUser User = (TblUser)Session["User"];
            // string ifadelerin onune gecmek icin
            int Id;
            if (User != null)
            {
                ViewBag.CartList = BusinessLayer.GetCartItems(
                    User.UserId,
                    out ProductInfos,
                    out OMesage
                );
                ViewBag.ProductInfos = ProductInfos;
            }

            return View(ViewBag);
        }
    }
}
/*
    Controller yazilirken is katmani uzerinden verilerin  cekilmesi saglandi,
    kullanıcıya ait userid routing kismindan cekilerek ilgili kullanicinin sepete listelenmeye calisildi,
    sepetteki urunlerin bilgileri icin is katmanından ilgili Cart tablosundan productId eslesen urunler cekildi
 */
