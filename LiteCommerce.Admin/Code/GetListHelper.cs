using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    public class GetListHelper
    {
        public static List<Product> ListProduct()
        {
            List<Product> list = CatalogBLL.Products_List(1,CatalogBLL.Product_Count("",0,0),"",0,0);
            return list;
        }
    }
}