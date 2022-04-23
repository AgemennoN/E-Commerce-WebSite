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
        //<<<Hüseyin Bilgiç - Start
        public List<TblCategory> GetCategories(out string OMessage)
        {
            List<TblCategory> Categories = new List<TblCategory>();
            OMessage = "";
            try
            {
                Categories = (from data in Context.TblCategories where data.CategoryActive == true select data).ToList();
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
                Products = (from data in Context.TblProducts where data.CategoryId == CategoryId && data.ProductActive == true select data).ToList();
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
        ///Vejdi BURAK - End >>>
        //<<< Vejdi BURAK - Start
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
        ///Fırat Seven - End >>>
        //<<<Fırat Seven - Start
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
        //AKIN CAN CESARETLI - END>>>

        //<<<AKIN CAN CESARETLI - START
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
        //AKIN CAN CESARETLI - END>>>

        //<<<AKIN CAN CESARETLI - START
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
                else if (User.UserPassword == Password)
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

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        //Admin Paneli Urunler Listesi Kodu
        public List<TblProduct> GetProductList(out string OMessage)
        {
            List<TblProduct> ProductList = new List<TblProduct>();
            OMessage = "";
            try
            {
                ProductList = (from Data in Context.TblProducts where Data.ProductActive == true select Data).ToList();
            }
            catch (Exception ex)
            {

                OMessage = ex.Message;
            }

            return ProductList;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        //Admin Paneli Kullanıcılar Listesi Kodu
        public List<TblUser> GetUserList(out string OMessage)
        {
            List<TblUser> UserList = new List<TblUser>();
            OMessage = "";
            try
            {
                UserList = (from Data in Context.TblUsers where Data.UserActive == true select Data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }

            return UserList;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        //Admin Paneli Aboneler Listesi Kodu
        public List<TblSubscriber> GetSubscriberList(out string OMessage)
        {
            List<TblSubscriber> SubscriberList = new List<TblSubscriber>();
            OMessage = "";
            try
            {
                SubscriberList = (from Data in Context.TblSubscribers select Data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }

            return SubscriberList;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        // Urun Ekleme Butonuna Tiklayinca Kategoriyi DropDown Cekmek İcin
        public List<TblCategory> GetCategoryList(out string OMessage)
        {
            List<TblCategory> CategoryList = new List<TblCategory>();
            OMessage = "";
            try
            {
                CategoryList = (from Data in Context.TblCategories where Data.CategoryActive == true orderby Data.CategoryName ascending select Data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return CategoryList;
        }


        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        // Admin Urun Ekleme Fonksiyonu
        public bool AddProduct(out string OMessage, TblProduct Product)
        {
            OMessage = "";
            bool IsAdded = false;
            try
            {
                Context.TblProducts.Add(Product);
                Context.SaveChanges();
                IsAdded = true;
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }

            return IsAdded;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        //Admin Kategori Ekleme Fonskiyonu
        public bool AddCategory(out string OMessage, TblCategory Category)
        {
            OMessage = "";
            bool IsAdded = false;
            try
            {
                Context.TblCategories.Add(Category);
                Context.SaveChanges();
                IsAdded = true;
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }

            return IsAdded;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        public List<TblOrder> GetOrderList(out string OMessage)
        {
            List<TblOrder> OrderList = new List<TblOrder>();
            OMessage = "";
            try
            {
                OrderList = (from Data in Context.TblOrders select Data).ToList();
            }
            catch (Exception ex)
            {

                OMessage = ex.Message;
            }
            return OrderList;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        public List<TblOrder> GetNonDeliveredOrderList(out string OMessage)
        {
            List<TblOrder> OrderList = new List<TblOrder>();
            OMessage = "";
            try
            {
                OrderList = (from Data in Context.TblOrders where Data.IsDelivered == false select Data).ToList();
            }
            catch (Exception ex)
            {

                OMessage = ex.Message;
            }
            return OrderList;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        public bool ProductDeleteFromDb(int ProductId, out string OMessage)
        {
            bool Success = false;
            OMessage = "";
            try
            {
                TblProduct Product = (from Data in Context.TblProducts where Data.ProductId == ProductId select Data).FirstOrDefault();
                if (Product == null)
                {
                    OMessage = "Böyle bir ürün Veri tabanında kayıtlı değil.";
                }
                else
                {
                    Context.TblProducts.Where(x => x.ProductId == ProductId).ToList().ForEach(x => x.ProductActive = false);
                    Context.SaveChanges();
                    Success = true;
                    OMessage = "#" + ProductId.ToString() + " " + Product.ProductName + " isimli ürün silindi.";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Success;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        public bool CategoryDeleteFromDb(int CategoryId, out string OMessage)
        {
            bool Success = false;
            OMessage = "";
            try
            {
                TblCategory Category = (from Data in Context.TblCategories where Data.CategoryId == CategoryId select Data).FirstOrDefault();
                if (Category == null)
                {
                    OMessage = "Böyle bir Kategori Veri tabanında kayıtlı değil.";
                }
                else
                {
                    Context.TblCategories.Where(x => x.CategoryId == CategoryId).ToList().ForEach(x => x.CategoryActive = false);
                    Context.SaveChanges();
                    Success = true;
                    OMessage = "#" + CategoryId.ToString() + " " + Category.CategoryName + " Kategori silindi.";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Success;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        public bool UserDeleteFromDb(int UserId, out string OMessage)
        {
            bool Success = false;
            OMessage = "";
            try
            {
                TblUser User = (from Data in Context.TblUsers where Data.UserId == UserId select Data).FirstOrDefault();
                if (User == null)
                {
                    OMessage = "Böyle bir Kullanıcı Veri tabanında kayıtlı değil.";
                }
                else
                {
                    Context.TblUsers.Where(x => x.UserId == UserId).ToList().ForEach(x => x.UserActive = false);
                    Context.SaveChanges();
                    Success = true;
                    OMessage = "#" + UserId.ToString() + " " + User.UserName + " adlı Kullanıcı silindi.";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Success;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        public bool SubscriberDeleteFromDb(int SubscriberId, out string OMessage)
        {
            bool Success = false;
            OMessage = "";
            try
            {
                TblSubscriber Subscriber = (from Data in Context.TblSubscribers where Data.Id == SubscriberId select Data).FirstOrDefault();
                if (Subscriber == null)
                {
                    OMessage = "Böyle bir Abone Veri tabanında kayıtlı değil.";
                }
                else
                {
                    Context.TblSubscribers.Remove(Subscriber);
                    Context.SaveChanges();
                    Success = true;
                    OMessage = "#" + SubscriberId.ToString() + " " + Subscriber.MailAddress + " adresli Abone silindi.";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Success;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>


        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        public TblProduct GetProductByProductId(int ProductId, out string OMessage)
        {
            TblProduct Product = new TblProduct();
            OMessage = "";
            try
            {
                Product = (from Data in Context.TblProducts where Data.ProductId == ProductId select Data).FirstOrDefault();
                if (Product == null)
                {
                    OMessage = "Böyle bir ürün Veri tabanında kayıtlı değil.";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Product;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>


    }
}
