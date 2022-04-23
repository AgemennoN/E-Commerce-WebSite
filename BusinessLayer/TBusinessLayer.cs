using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalDataLayer;

namespace BusinessLayer
{
    public class TBusinessLayer
    {
        private DbManavMelihEntities Context;
        public TBusinessLayer()
        {
            Context = new DbManavMelihEntities();
        }

        //<<<AKIN CAN CESARETLI - START
        public List<TblUser> GetUsers(out string Omessage)
        {
            List<TblUser> result = new List<TblUser>();
            Omessage = "";
            try
            {
                result = (from user in Context.TblUsers select user).ToList();
            }
            catch (Exception ex)
            {

                Omessage = ex.Message;
            }

            return result;


        }
        
        public bool AddUser(TblUser User, out string Omessage)
        {
            Omessage = "";
            bool result = false;
            try
            {
                TblUser Data = (from data in Context.TblUsers where data.UserName == User.UserName && data.UserActive == true select data).FirstOrDefault();
                if (Data == null)
                {
                    Context.TblUsers.Add(User);
                    Context.SaveChanges();
                    result = true;
                    Omessage = "Yeni Kayıt Başarılı";
                }
                else
                {
                    Omessage = "Kullanıcı ismi zaten kullanılıyor";
                }
                
            }
            catch (Exception ex)
            {

                Omessage = ex.Message;
            }
            return result;
        }
        public TblUser Login(string Username, string Password, out string Omessage)
        {
            Omessage = "";
            TblUser result = null;
            TblUser User = (from user in Context.TblUsers where user.UserName == Username && user.UserActive == true select user).FirstOrDefault();
            if (User == null)
            {
                Omessage = "Giriş bilgileri hatalı";
            }
            else if (User.UserName == Username)
            {
                if (User.UserPassword != Password)
                {
                    Omessage = "Giriş bilgileri hatalı";
                }
                else if(User.UserPassword == Password)
                {
                    result = User;
                    Omessage = "Giriş başarılı";
                   
                }
            }

            return result;
        }        
     

        //AKIN CAN CESARETLI - END>>>
    }
}
