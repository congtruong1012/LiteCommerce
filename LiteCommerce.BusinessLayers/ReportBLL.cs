using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class ReportBLL
    {
        private static IOrderDAL OrderDB { set; get; }
        private static IOrderDetailDAL OrderDetailDB { set; get; }
        public static void Initialize(string connectionString)
        {
            OrderDB = new DataLayers.SQLServer.OrderDAL(connectionString);
            OrderDetailDB = new DataLayers.SQLServer.OrderDetailDAL(connectionString);
        }
        public static List<Order> Order_List(int page, int pageSize, string customerID, int employeeID, int shippID)
        {
            return OrderDB.List(page, pageSize, customerID, employeeID, shippID);
        }
        public static int Order_Count(int page, int pageSize, string customerID, int employeeID, int shippID)
        {
            return OrderDB.Count(page, pageSize, customerID, employeeID, shippID);
        }
        public static List<OrderDetail> OrderDetail_List(int orderID)
        {
            return OrderDetailDB.List(orderID);
        }
        public static bool Order_Delete(int[] orderIDs)
        {
            return OrderDB.Delete(orderIDs);
        }
        public static Order Order_Get(int orderID)
        {
            return OrderDB.Get(orderID);
        }
    }
}
