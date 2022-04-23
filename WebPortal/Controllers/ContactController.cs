using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    #region Busra Şimşek
    public class ContactController : Controller
    {
        private TBusinessLayer _bussines;
        //Dependency injection ile alınması gerekiyor. Aşağıdaki gibi newlemekten daha doğru.
        public ContactController()
        {
            _bussines = new TBusinessLayer();
        }
        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(WebPortalContactModel model)
        {
            //Çift maplama yaptık. Entitiylere business üzerinden erişmek yanlış diye bussines içine bir model ekledik
            //WebPortal içine de model ekledik. Bunları maplayarak gönderdik
            string message = "";
            var addContactMessage = _bussines.AddContact(new BussinesContactModel
            {
                Name = model.Name,
                Phone = model.Phone,
                Mail = model.Mail,
                Message = model.Message,
                Subject = model.Subject,
            });
            ViewBag.Message = addContactMessage;
            return View("Index");
        }
    }
    #endregion  
}