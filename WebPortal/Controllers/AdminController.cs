using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] Admin Sayfasi Urunler Listesi Yapildi
        public ActionResult Index()
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
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] Admin Sayfasi Urunler Listesi Yapildi
    }
}