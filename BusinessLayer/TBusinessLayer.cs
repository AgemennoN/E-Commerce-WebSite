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
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] - End >>>


    }
}
