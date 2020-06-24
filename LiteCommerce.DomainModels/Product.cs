using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Product
    {
        public int ProductID { set; get; }
        public string ProductName { set; get; }
        public int SupplierID { set; get; }
	    public int CategoryID { set; get; }
	    public string QuantityPerUnit { set; get; }
	    public double UnitPrice { set; get; }
	    public string Descriptions { set; get; }
        public string PhotoPath { set; get; }
    }
}
