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
        //<<<Huseyin Bilgic - Start
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
        //Huseyin Bilgic - End>>>

        //<<<Huseyin Bilgic - Start
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
        //Huseyin Bilgic - End>>>

        //<<<Huseyin Bilgic - Start
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
        //Huseyin Bilgic - End>>>

        //<<<[YK]
        // Overload of Huseyin Bilgic's GetCategoryById function, Instead of String parameter this one gets Int parameter 
        public TblCategory GetCategoryById(int CategoryId, out string OMessage)
        {
            TblCategory Category = new TblCategory();
            OMessage = "";
            try
            {
                Category = (from data in Context.TblCategories where data.CategoryId == CategoryId select data).FirstOrDefault();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;

            }
            return Category;
        }
        //[YK] - End>>>

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

        //<<<Firat Seven - Start
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
        ///Firat Seven - End >>>
        //<<<Firat Seven - Start
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
        ///Firat Seven - End >>>

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
                    Omessage = "Yeni Kayit Basarili";
                }
                else
                {
                    Omessage = "Kullanici ismi zaten kullaniliyor";
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
                Omessage = "Giris bilgileri hatali";
            }
            else if (User.UserName == Username)
            {
                if (User.UserPassword != Password)
                {
                    Omessage = "Giris bilgileri hatali";
                }
                else if (User.UserPassword == Password)
                {
                    result = User;
                    Omessage = "Giris basarili";

                }
            }

            return result;
        }
        //AKIN CAN CESARETLI - END>>>

        //<<<Belgin coban - Start
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
        //Belgin coban - End>>>

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
        //Admin Paneli Kullanicilar Listesi Kodu
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
                OMessage = "Urun Basariyla eklendi";
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
                    OMessage = "Böyle bir Urun Veri tabaninda kayitli degil.";
                }
                else
                {
                    Context.TblProducts.Where(x => x.ProductId == ProductId).ToList().ForEach(x => x.ProductActive = false);
                    Context.SaveChanges();
                    Success = true;
                    OMessage = "#" + ProductId.ToString() + " " + Product.ProductName + " isimli Urun silindi.";
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
                    OMessage = "Böyle bir Kategori Veri tabaninda kayitli degil.";
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
                    OMessage = "Böyle bir Kullanici Veri tabaninda kayitli degil.";
                }
                else
                {
                    Context.TblUsers.Where(x => x.UserId == UserId).ToList().ForEach(x => x.UserActive = false);
                    Context.SaveChanges();
                    Success = true;
                    OMessage = "#" + UserId.ToString() + " " + User.UserName + " Adli uye silindi.";
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
                    OMessage = "Böyle bir Urun Veri tabaninda kayitli degil.";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Product;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        // Admin Urun Duzenleme Fonksiyonu
        public bool EditProduct(TblProduct Product, out string OMessage)
        {
            OMessage = "";
            bool IsEdited = false;
            try
            {
                TblProduct data = (from Data in Context.TblProducts where Data.ProductId == Product.ProductId select Data).FirstOrDefault();
                if (data != null)
                {
                    data.ProductName = Product.ProductName;
                    data.ProductPrice = Product.ProductPrice;
                    data.ProductDiscount = Product.ProductDiscount;
                    data.PriceOnSale = Product.PriceOnSale;
                    data.ProductImage = Product.ProductImage;
                    data.ProductStock = Product.ProductStock;
                    data.CategoryId = Product.CategoryId;
                    Context.SaveChanges();
                    IsEdited = true;
                    OMessage = "Urun Detaylari Basariyla Degistirildi";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }

            return IsEdited;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        // Admin Kategori Duzenleme Fonksiyonu
        public bool EditCategory(TblCategory Category, out string OMessage)
        {
            OMessage = "";
            bool IsEdited = false;
            try
            {
                TblCategory data = (from Data in Context.TblCategories where Data.CategoryId == Category.CategoryId select Data).FirstOrDefault();
                if (data != null)
                {
                    data.CategoryName = Category.CategoryName;
                    Context.SaveChanges();
                    IsEdited = true;
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }

            return IsEdited;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>


        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        // From Db changes TblOrder's IsDelivered to True
        public bool OrderDeliverById(int OrderId, out string OMessage)
        {
            bool Success = false;
            OMessage = "";
            try
            {
                TblOrder Order = (from Data in Context.TblOrders where Data.OrderId == OrderId select Data).FirstOrDefault();
                if (Order == null)
                {
                    OMessage = "Böyle bir Siparis Veri tabaninda kayitli degil.";
                }
                else
                {
                    Context.TblOrders.Where(x => x.OrderId == OrderId).ToList().ForEach(x => x.IsDelivered = true);
                    Context.SaveChanges();
                    Success = true;
                    OMessage = "#" + OrderId.ToString() + " " + Order.Name + " isimli\n" + Order.OrderDateTime + " Tarihli Siparis Teslim Edildi.";
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
        public TblOrder GetOrderByOrderId(int OrderId, out string OMessage)
        {
            TblOrder Order = new TblOrder();
            OMessage = "";
            try
            {
                Order = (from Data in Context.TblOrders where Data.OrderId == OrderId select Data).FirstOrDefault();
                if (Order == null)
                {
                    OMessage = "Böyle bir Siparis Veri tabaninda kayitli degil.";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Order;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        public List<TblCart> GetCartsInsideTheOrder(TblOrder Order)
        {
            List<TblCart> Carts = new List<TblCart>();
            foreach (string StrCartId in Order.OrderedCarts.Split(','))
            {
                if (StrCartId != "")
                {
                    int IntCartId;
                    Int32.TryParse(StrCartId, out IntCartId);
                    if (IntCartId != 0)
                    {
                        TblCart Cart = (from Data in Context.TblCarts where Data.CartId == IntCartId select Data).FirstOrDefault();
                        Carts.Add(Cart);
                    }
                }
            }
            return Carts;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>


        //<<<[EGEMEN-GOKHAN-MELIH-TAYFUN] - Start
        // Gets User even if User.Active == False;
        public TblUser GetUserByUserId(int UserId)
        {
            TblUser User = null;

            User = (from Data in Context.TblUsers where Data.UserId == UserId select Data).FirstOrDefault();

            return User;
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
                    OMessage = "Böyle bir Abone Veri tabaninda kayitli degil.";
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
        public List<TblContact> GetContacts(out string OMessage)
        {
            List<TblContact> Messages = new List<TblContact>();
            OMessage = "";
            try
            {
                Messages = (from data in Context.TblContacts select data).ToList();
                if (Messages == null)
                {
                    OMessage = "Gösterilecek Mesaj yok";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }

            return Messages;
        }

        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>

        public bool ContactMessageDelete(int MessageId, out string OMessage)
        {
            OMessage = "Mesaj Silinemedi";
            bool result = false;
            try
            {
                TblContact Contact = (from Data in Context.TblContacts where Data.ContactId == MessageId select Data).FirstOrDefault();
                if (Contact != null)
                {
                    Context.TblContacts.Remove(Contact);
                    Context.SaveChanges();
                    OMessage = "Mesaj Silindi";
                    result = true;
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return result;
        }

        public TblContact GetContactById(int Id, out string OMessage)
        {
            OMessage = "Mesaj Bulundu";
            TblContact Contact = new TblContact();
            try
            {
                Contact = (from Data in Context.TblContacts where Data.ContactId == Id select Data).FirstOrDefault();
                if (Contact != null)
                {
                    OMessage = "Mesaj Bulundu";
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Contact;
        }

        //BUKET SOYHAN
        public bool RemoveSubscriber(string MailAddress, out string OMessage)
        {
            OMessage = "";
            bool result = false;
            try
            {
                TblSubscriber Subscriber = (from Subs in Context.TblSubscribers where Subs.MailAddress == MailAddress select Subs).FirstOrDefault();
                Context.TblSubscribers.Remove(Subscriber);
                Context.SaveChanges();
                OMessage = MailAddress + " adresi abonelikten cikarildi";
                result = true;
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return result;
        }
    }
}
