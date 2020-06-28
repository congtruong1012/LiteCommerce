using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IOrderDAL
    {
        List<Order> List(int page, int pageSize, string customerID, int employeeID, int shipperID);
        Order Get(int orderID);
        int Count(int page, int pageSize, string customerID, int employeeID, int shipperID);
        bool Delete(int[] orderIDs);
    }
}
