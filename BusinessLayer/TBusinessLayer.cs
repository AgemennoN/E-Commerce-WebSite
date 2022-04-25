using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
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
        //<<<Hüseyin Bilgiç - Start
        public List<TblCategory> GetCategories(out string OMessage)
        {
            List<TblCategory> Categories = new List<TblCategory>();
            OMessage = "";
            try
            {
                Categories=(from data in Context.TblCategories where data.CategoryActive==true select data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;

            }
            return Categories;
        }
        //Hüseyin Bilgiç - End>>>

        //<<<Hüseyin Bilgiç - Start
        public List<TblProduct> GetProductsByCategoryId(int CategoryId, out string OMessage)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (from data in Context.TblProducts where data.CategoryId == CategoryId &&data.ProductActive==true select data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;

            }
            return Products;
        }
        //Hüseyin Bilgiç - End>>>

        //<<<Hüseyin Bilgiç - Start
        public TblCategory GetCategoryById(string CategoryName, out string OMessage)
        {
            TblCategory Category = new TblCategory();
            OMessage = "";
            try
            {
                Category = (from data in Context.TblCategories where data.CategoryName == CategoryName select data).FirstOrDefault();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;

            }
            return Category;
        }
        //Hüseyin Bilgiç - End>>>

        //<<< Vejdi BURAK - Start
        public List<TblProduct> GetProducts(out string OMessage)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (from Data in Context.TblProducts where Data.ProductActive == true select Data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }
        public List<TblProduct> GetFoundProducts(out string OMessage, string UrunAdi)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (from Data in Context.TblProducts where (Data.ProductName.ToLower()).Contains(UrunAdi) && Data.ProductActive == true select Data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }
        public List<TblProduct> GetFoundLowProducts(out string OMessage, string UrunAdi)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (from Data in Context.TblProducts where (Data.ProductName.ToLower()).Contains(UrunAdi) && Data.ProductActive == true orderby Data.PriceOnSale select Data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }
        public List<TblProduct> GetFoundHighProducts(out string OMessage, string UrunAdi)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (from Data in Context.TblProducts where (Data.ProductName.ToLower()).Contains(UrunAdi) && Data.ProductActive == true orderby Data.PriceOnSale descending select Data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }
        ///Vejdi BURAK - End >>>
        
        //<<<Fırat Seven - Start
        public List<TblProduct> GetLowPrice(out string OMessage)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (from DataLow in Context.TblProducts where DataLow.ProductActive == true orderby DataLow.PriceOnSale select DataLow).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }
        public List<TblProduct> GetHighPrice(out string OMessage)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (from DataLow in Context.TblProducts where DataLow.ProductActive == true orderby DataLow.PriceOnSale descending select DataLow).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }
        ///Fırat Seven - End >>>

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

        //<<<Belgin Çoban - Start
        public List<TblProduct> GetProducDiscounts(out string OMessage)
        {
            List<TblProduct> products = new List<TblProduct>();
            OMessage = "";
            try
            {
                products = (from data in Context.TblProducts.Where(p => p.ProductDiscount > 0).OrderByDescending(p => p.ProductDiscount) select data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;

            }
            return products;
        }
        //Belgin Çoban - End>>>

        #region Buşra Şimşek Contact Add Function
        public string AddContact(BussinesContactModel contactModel)
        {
            //Automapper kullanılabilir..
            Context.TblContacts.Add(new TblContact
            {
                ContactName = contactModel.Name,
                ContactMail = contactModel.Mail,
                ContactPhone = contactModel.Phone,
                ContactMessage = contactModel.Message,
                ContactSubject = contactModel.Subject,
            });
            //Save yapıyoruz... Save olmama durumunda geriye false döndürüyoruz
            try
            {
                Context.SaveChanges();
                return "İşlem başarıyla gerçekleşti";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
        #endregion

        //<<<Buket Soyhan - START

        public bool IsSubscriber(string MailAddress, out string OMessage)
        {
            OMessage = "";
            bool result = false;
            try
            {
                TblSubscriber Subscriber = (from Subs in Context.TblSubscribers where Subs.MailAddress == MailAddress select Subs).FirstOrDefault();
                if (Subscriber != null)
                {
                    result = true;
                    OMessage = MailAddress + " adresi zaten kayıt edilmiş.";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return result;
        }

        public bool AddSubscriber(string MailAddress, out string OMessage)
        {
            OMessage = "";
            bool result = false;
            try
            {
                TblSubscriber NewSubscriber = new TblSubscriber();
                NewSubscriber.MailAddress = MailAddress;
                Context.TblSubscribers.Add(NewSubscriber);
                Context.SaveChanges();
                OMessage = MailAddress + " adresin abonelik islemi gerceklestirildi.";
                result = true;
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return result;
        }

        public bool RemoveSubscriber(string MailAddress, out string OMessage)
        {
            OMessage = "";
            bool result = false;
            try
            {
                TblSubscriber Subscriber = (from Subs in Context.TblSubscribers where Subs.MailAddress == MailAddress select Subs).FirstOrDefault();
                Context.TblSubscribers.Remove(Subscriber);
                Context.SaveChanges();
                OMessage = MailAddress + " adresi abonelikten çıkarıldı";
                result = true;
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return result;
        }

        //Buket Soyhan - END>>>

    }
}
