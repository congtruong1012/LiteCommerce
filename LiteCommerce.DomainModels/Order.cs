using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Order
    {
        public int OrderID { set; get; }
        public string CustomerID { set; get; }
        public int EmployeeID { set; get; }
        public DateTime OrderDate { set; get; }
        public DateTime RequiredDate { set; get; }
        public DateTime ShippedDate { set; get; }
        public int ShipperID { set; get; }
        public double Freight { set; get; }
        public string ShipAddress { set; get; }
        public string ShipCity { set; get; }
        public string ShipCountry { set; get; }
    }
}
