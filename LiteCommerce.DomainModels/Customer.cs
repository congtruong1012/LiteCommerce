using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Customer
    {
       public string CustomerID { set; get; } 
       public string CompanyName { set; get; }
	   public string ContactName { set; get; }
	   public string ContactTitle { set; get; }
	   public string Address { set; get; }
	   public string City { set; get; }
	   public string Country { set; get; }
	   public string Phone { set; get; }
	   public string Fax { set; get; }
    }
}
