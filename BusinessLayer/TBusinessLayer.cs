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
                Context.TblUsers.Add(User);
                Context.SaveChanges();
                result = true;
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
                where user.UserName == Username
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
                    Omessage = "Giriş başarılı";
                    result = User;
                }
            }

            return result;
        }

        //AKIN CAN CESARETLI - END>>>
        //Hüseyin Bilgiç ekledi.
        public List<TblCategory> GetCategories(out string OMessage)
        {
            List<TblCategory> Categories = new List<TblCategory>();
            OMessage = "";
            try
            {
                Categories = (from data in Context.TblCategories select data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Categories;
        }

        //Hüseyin Bilgiç ekledi.
        public List<TblProduct> GetProductsByCategoryId(int CategoryId, out string OMessage)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (
                    from data in Context.TblProducts
                    where data.CategoryId == CategoryId
                    select data
                ).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }

        //Hüseyin Bilgiç ekledi.
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

        //hhuseyin.demirtas
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
                    where CartItems.UserId == UserId
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

        public void AddToCartTable(TblCart CartItem, out string OMessage)
        {
            TBusinessLayer BusinessLayer = new TBusinessLayer();
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
    }
}
