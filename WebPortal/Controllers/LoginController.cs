using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using PortalDataLayer;

namespace WebPortal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        //<<<AKIN CAN CESARETLI - START
        public ActionResult Index()
        {
            

            return View();
        }        
        public ActionResult Login() //Login icin controller oluşturuldu. Formdan alınan bilgilerle login islemi yapıldı.
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string Omessage;
            List<TblUser> UserList = BusinessLayer.GetUsers(out Omessage);


            string Username = Request.Form["TxtUsernameLogin"].ToString();
            string Password = Request.Form["TxtPasswordLogin"].ToString();
            TblUser User = BusinessLayer.Login(Username,Password,out Omessage);
            Session["User"] = User;
            if (User != null)
            {
                if (User.UserId == 2)
                {
                    Session["Admin"] = true;
                }
                return new RedirectResult("~/Home");
            }
            else
            {
                return new RedirectResult("~/Login");
            }

            
            
        }
        public ActionResult Register() //Kayıt işlemi için controller oluşturuldu. Formdan bilgiler alınıp veri tabanına eklendi.
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string Omessage;
            List<TblUser>UserList = BusinessLayer.GetUsers(out Omessage);

            string Username = Request.Form["TxtUsername"].ToString();
            string Password = Request.Form["TxtPassword"].ToString();
            string Email = Request.Form["TxtEmail"].ToString();
            string PhoneNumber = Request.Form["TxtPhone"].ToString();

           
            TblUser User = new TblUser()
            {
                UserName = Username,
                UserPassword = Password,
                UserMail = Email,
                UserPhoneNumber = PhoneNumber,
                

            };
           
            bool Success = BusinessLayer.AddUser(User,out Omessage);
            TempData["Success"] = Success;
            TempData["Message"] = Omessage;

            return new RedirectResult("~/Home");
        }

        public ActionResult Logout()
        {
            Session["Admin"] = false;
            Session["User"] = null;
            return new RedirectResult("~/Home");
        }
        //AKIN CAN CESARETLI - END>>>

    }
}