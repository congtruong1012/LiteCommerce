using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class OrderDetail
    {
        public int OrderID { set; get; }
        public int ProductID { set; get; }
        public double UnitPrice { set; get; }
        public int Quantity { set; get; }
        public double Discount { set; get; }
    }
}
