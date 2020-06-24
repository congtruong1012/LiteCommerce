using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class OrderPaginationResult: PaginationResult
    {
        public List<Order> Data;
        public string customerID;
        public int employeeID;
        public int shipperID;
    }
}