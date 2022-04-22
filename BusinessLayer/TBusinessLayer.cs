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
        //<<< Vejdi BURAK - Start
        public List<TblProduct> GetProducts(out string OMessage)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (from Data in Context.TblProducts select Data).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }
        

        ///Vejdi BURAK - End >>>

        public List<TblProduct> GetLowPrice(out string OMessage)
        {
            List<TblProduct> Products = new List<TblProduct>();
            OMessage = "";
            try
            {
                Products = (from DataLow in Context.TblProducts where DataLow.ProductActive==true orderby DataLow.ProductPrice select DataLow).ToList();
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
                Products = (from DataLow in Context.TblProducts where DataLow.ProductActive == true orderby DataLow.ProductPrice descending select DataLow).ToList();
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }


    }
}
