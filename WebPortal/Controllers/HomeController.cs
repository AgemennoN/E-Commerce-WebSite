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
    }
}
