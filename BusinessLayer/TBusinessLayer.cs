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
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] Admin Sayfasi Urunler Listesi Kodu
        private DbManavMelihEntities Context;
        public TBusinessLayer()
        {
            Context = new DbManavMelihEntities();
        }

        public List<TblProduct> GetProductList(out string OMessage)
        {
            List<TblProduct> ProductList = new List<TblProduct>();
            OMessage = "";
            try
            {
                ProductList = (from Data in Context.TblProducts where Data.ProductActive == true  select Data).ToList();
            }
            catch (Exception ex)
            {

                OMessage = ex.Message;
            }

            return ProductList;
        }
        //[EGEMEN-GOKHAN-MELIH-TAYFUN] Admin Sayfasi Urunler Listesi Kodu

    }
}
