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

        public bool ProductDeleteFromDb(int ProductId, out string OMessage)
        {
            bool Success = false;
            OMessage = "";
            try
            {
                TblProduct Product = (from Data in Context.TblProducts where Data.ProductId == ProductId select Data).FirstOrDefault();
                if(Product == null)
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

    }
}
