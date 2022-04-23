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

    }
}
