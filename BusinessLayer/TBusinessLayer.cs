﻿using System;
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
                Products = (from Data in Context.TblProducts where Data.ProductActive==true select Data).ToList();
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
                Products = (from Data in Context.TblProducts where Data.ProductName == UrunAdi select Data).ToList();
                //var Product = Context.Database.SqlQuery("select * from TblProducts where ProductName like '%" + UrunAdi + "%'");
            }
            catch (Exception ex)
            {
                OMessage = ex.Message;
            }
            return Products;
        }


        ///Vejdi BURAK - End >>>



    }
}
