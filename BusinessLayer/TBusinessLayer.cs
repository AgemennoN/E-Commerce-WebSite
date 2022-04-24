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
                Categories = (
                    from data in Context.TblCategories
                    where data.CategoryActive == true
                    select data
                ).ToList();
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
                Products = (
                    from data in Context.TblProducts
                    where data.CategoryId == CategoryId && data.ProductActive == true
                    select data
                ).ToList();
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
                Category = (
                    from data in Context.TblCategories
                    where data.CategoryName == CategoryName
                    select data
                ).FirstOrDefault();
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
                Products = (
                    from Data in Context.TblProducts
                    where Data.ProductActive == true
                    select Data
                ).ToList();
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
                Products = (
                    from Data in Context.TblProducts
                    where
                        (Data.ProductName.ToLower()).Contains(UrunAdi) && Data.ProductActive == true
                    select Data
                ).ToList();
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
                Products = (
                    from DataLow in Context.TblProducts
                    where DataLow.ProductActive == true
                    orderby DataLow.PriceOnSale
                    select DataLow
                ).ToList();
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
                Products = (
                    from DataLow in Context.TblProducts
                    where DataLow.ProductActive == true
                    orderby DataLow.PriceOnSale descending
                    select DataLow
                ).ToList();
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
                TblUser Data = (
                    from data in Context.TblUsers
                    where data.UserName == User.UserName && data.UserActive == true
                    select data
                ).FirstOrDefault();
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
            TblUser User = (
                from user in Context.TblUsers
                where user.UserName == Username && user.UserActive == true
                select user
            ).FirstOrDefault();
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
                products = (
                    from data in Context.TblProducts
                        .Where(p => p.ProductDiscount > 0)
                        .OrderByDescending(p => p.ProductDiscount)
                    select data
                ).ToList();
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
            Context.TblContacts.Add(
                new TblContact
                {
                    ContactName = contactModel.Name,
                    ContactMail = contactModel.Mail,
                    ContactPhone = contactModel.Phone,
                    ContactMessage = contactModel.Message,
                    ContactSubject = contactModel.Subject,
                }
            );
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

        #region hhuseyin.demirtas tarafından eklenen kısım
        //hhuseyin.demirtas --- User id ile eşleşen cart nesnelerinin veri tabanından çekilmesi
        public List<TblCart> GetCartItems(
            int UserId,
            out List<TblProduct> ProductInfos,
            out string OMessage
        )
        {
            List<TblCart> CartList = new List<TblCart>();
            ProductInfos = new List<TblProduct>();
            OMessage = "";
            try
            {
                CartList = (
                    from CartItems in Context.TblCarts
                    where (CartItems.UserId == UserId && CartItems.IsOrdered == 0)
                    select CartItems
                ).ToList();

                foreach (TblCart cartItem in CartList)
                {
                    ProductInfos.Add(
                        (
                            from Product in Context.TblProducts
                            where Product.ProductId == cartItem.ProductId
                            select Product
                        ).FirstOrDefault()
                    );
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return CartList;
        }

        //hhuseyin.demirtas --- Sessiondaki UserId icin sepet nesnesinin varliginin kontrolu
        public bool CartIsEmpty(int UserId)
        {
            bool result = false;
            try
            {
                var HaveItems = (
                    from CartItems in Context.TblCarts
                    where (CartItems.UserId == UserId && CartItems.IsOrdered == 0)
                    select CartItems
                ).ToList();

                if (HaveItems.Count != 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        //hhuseyin.demirtas --- ürün tablosundaki cartId ile gelen ürün cart tablosunda inactive olarak guncellenmesi.
        public void DeleteFromCart(int CartId, out string OMessage)
        {
            OMessage = "";
            try
            {
                Context.TblCarts
                    .Where(x => x.CartId == CartId)
                    .ToList()
                    .ForEach(x => x.IsOrdered = -1);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
        }

        //hhuseyin.demirtas --- ürün tablosuna CartItem nesnesinin eklenmesi
        public void AddToCartTable(TblCart CartItem, out string OMessage)
        {
            OMessage = "";
            try
            {
                if (ProductExist(CartItem, out OMessage))
                {
                    CartItem = (
                        from Data in Context.TblCarts
                        where Data.UserId == CartItem.UserId && Data.ProductId == CartItem.ProductId
                        select Data
                    ).FirstOrDefault();
                    Context.TblCarts
                        .Where(x => x.CartId == CartItem.CartId)
                        .ToList()
                        .ForEach(x => x.Quantity++);
                    Context.SaveChanges();
                }
                else
                {
                    Context.TblCarts.Add(CartItem);
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
        }

        //hhuseyin.demirtas --- cart tablosu icerisinde aynı userid ve product id'ye ait kaydın kontrol edilmesi...
        public bool ProductExist(TblCart CartItem, out string OMessage)
        {
            bool result = false;
            OMessage = "";
            try
            {
                TblCart Data = (
                    from Search in Context.TblCarts
                    where Search.UserId == CartItem.UserId && Search.ProductId == CartItem.ProductId
                    select Search
                ).FirstOrDefault();
                if (Data != null)
                    result = true;
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }

            return result;
        } //hhuseyin.demirtas end
        #endregion

        #region Celal Serdar Ergun Metod ve Fonksiyonlar
        /*
         * Celal Serdar Ergun ekledi
         * Urun sepete eklenir
         */
        public bool AddProductToCart(int ProductId, int UserId, out string OMessage)
        {
            OMessage = "";
            bool Result = false;
            string StockMessage;
            try
            {
                int CartId;
                TblProduct Product = ProductInfo(ProductId, out StockMessage);
                // Urun sepette varsa sayısını ve toplam tutarı arttırır
                // Yoksa urun olusturup veritabanına ekler
                if (IsProductInCart(ProductId, UserId, out CartId, out OMessage) == true)
                {
                    TblCart Cart = (
                        from item in Context.TblCarts
                        where item.CartId == CartId
                        select item
                    ).FirstOrDefault();
                    if ((int)Cart.Quantity <= Product.ProductStock)
                    {
                        Context.TblCarts
                            .Where(x => x.CartId == CartId)
                            .ToList()
                            .ForEach(x => x.Quantity++);
                        Context.TblCarts
                            .Where(x => x.CartId == CartId)
                            .ToList()
                            .ForEach(x => x.Price += (x.Price / (x.Quantity - 1)));
                        OMessage = "Sipariş eklendi";
                    }
                    else
                    {
                        OMessage = "Hata! Siparis eklenemedi";
                    }
                }
                else
                {
                    TblCart Cart = new TblCart();
                    Cart.UserId = UserId;
                    Cart.ProductId = ProductId;
                    Cart.Price = Product.ProductPrice;
                    Cart.Quantity = 1;
                    Cart.IsOrdered = 0;
                    Cart.DateTime = DateTime.Now;
                    Context.TblCarts.Add(Cart);
                    OMessage = "Siparis eklendi";
                }
                Context.SaveChanges();
                Result = true;
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Result;
        }

        /*
         * Celal Serdar Ergun ekledi
         * Urun sepette var mı kontrol ediyor
         */
        public bool IsProductInCart(int ProductId, int UserId, out int CartId, out string OMessage)
        {
            bool result = false;
            OMessage = "";
            CartId = 0;
            try
            {
                TblCart Item = (
                    from item in Context.TblCarts
                    where
                        item.ProductId == ProductId && item.UserId == UserId && item.IsOrdered == 0
                    select item
                ).FirstOrDefault();
                if (Item != null)
                {
                    result = true;
                    CartId = Convert.ToInt32(Item.CartId);
                }
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return result;
        }

        /*
         * Celal Serdar Ergun ekledi
         * Urun bilgilerini dondurur
         */
        public TblProduct ProductInfo(int ProductId, out string StockMessage)
        {
            StockMessage = "";
            TblProduct Result = new TblProduct();
            try
            {
                TblProduct Product = (
                    from item in Context.TblProducts
                    where item.ProductId == ProductId
                    select item
                ).FirstOrDefault();
                if (Product != null)
                {
                    Result = Product;
                }
            }
            catch (Exception ex)
            {
                StockMessage = ex.Message;
            }
            return Result;
        }
        #endregion

        #region Emre Tuzunoglu
        public bool AddPayment(TblOrder Ordered, out string OMessage)
        {
            OMessage = "";
            bool result = false;
            try
            {
                Context.TblOrders.Add(Ordered);
                Context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return result;
        }

        public bool isUpdatedCart(int UserId)
        {
            bool result = false;
            try
            {
                Context.TblCarts
                    .Where(x => x.UserId == UserId && x.IsOrdered == 0)
                    .ToList()
                    .ForEach(x => x.IsOrdered = 1);
                Context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        // arayuzden girilen degerler veri tabanina atilir. Daha sonra Her user id user id ye esitse ve isordered 0 ise her x icin is ordered degeri 1 e cekilir.
        //Emre Tuzunoglu End
        #endregion
    }
}
