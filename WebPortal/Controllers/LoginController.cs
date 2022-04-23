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
                TempData["LoginMessage"] = Omessage;
                return new RedirectResult("~/Home");
            }
            else
            {
                TempData["LoginMessage"] = Omessage;
                return new RedirectResult("~/Login");
            }

            
            
            
        }
        public ActionResult Register() //Kayıt işlemi için controller oluşturuldu. 
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
            string Omessage;           

            string Username = Request.Form["TxtUsername"].ToString();
            string Password = Request.Form["TxtPassword"].ToString();  //Formdan bilgiler alındı
            string Email = Request.Form["TxtEmail"].ToString();
            string PhoneNumber = Request.Form["TxtPhone"].ToString();


            TblUser User = new TblUser()
            {
                UserName = Username,
                UserPassword = Password,
                UserMail = Email,                       //Yeni oluşturulan User nesnesine formdan alınan bilgiler atandı
                UserPhoneNumber = PhoneNumber,
                UserActive = true,
                UserRegisterDate = DateTime.Now                

            };
           
            bool Success = BusinessLayer.AddUser(User,out Omessage);    //User nesnesi veritabanına eklendi mi diye kontrol edip Success değişkenine atanıyor

            TempData["LoginSuccess"] = Success;    //TempData kullanarak veriler index.cshtml'e taşınıyor
            TempData["LoginMessage"] = Omessage;

            return new RedirectResult("~/Login");
        }

        public ActionResult Logout()
        {
            string Omessage = "Cıkış yapıldı";
            Session["Admin"] = false;
            Session["User"] = null;
            TempData["LogoutMessage"] = Omessage;
            return new RedirectResult("~/Home");
        } //Çıkış yapıldı
        //AKIN CAN CESARETLI - END>>>

    }
}