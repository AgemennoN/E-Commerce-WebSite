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

        // hhuseyin.demirtas --- sepet sayfasının görüntülenmesi
        public ActionResult Index()
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            List<TblProduct> ProductInfos = new List<TblProduct>();
            string OMesage = "";
            TblUser User = (TblUser)Session["User"];

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

        //hhuseyin.demirtas --- sepet icerisinde bulunan urunun inactive duruma gecirilmesi
        public ActionResult DeleteProduct(string id)
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string OMesage = "";
            int cartId = Convert.ToInt32(id);
            BusinessLayer.DeleteFromCart(cartId, out OMesage);
            return new RedirectResult("~/Cart/Index");
        }
    }
}
/*
    Controller yazilirken is katmani uzerinden verilerin  cekilmesi saglandi,
    kullanıcıya ait userid Session nesnesi icerisinden cekilerek ilgili kullanicinin sepete listelenmeye calisildi,
    sepetteki urunlerin bilgileri icin is katmanından ilgili Cart tablosundan productId eslesen urunler cekildi
 */
