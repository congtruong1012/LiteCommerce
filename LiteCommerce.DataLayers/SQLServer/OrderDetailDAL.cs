using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class OrderDetailDAL: IOrderDetailDAL
    {
        private string connectionString;
        public OrderDetailDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<OrderDetail> List(int orderID)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            using (SqlConnection connection = new SqlConnection(connectionString)) //Tạo đối tượng kết nối CSDL
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())//Thực hiện truy vấn CSDL
                {
                    cmd.CommandText = @"select * from OrderDetails where OrderID = @orderID
                                        ";//Câu truy vấn
                    cmd.CommandType = System.Data.CommandType.Text; //Cho biết lệnh mà ta sd là lệnh gì
                    cmd.Connection = connection;//lệnh kết nối
                    cmd.Parameters.AddWithValue("@orderID", orderID);
                    using (SqlDataReader dbreader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (dbreader.Read())
                        {
                            OrderDetail orderDetail = new OrderDetail()
                            {
                                OrderID = Convert.ToInt32(dbreader["OrderID"]),
                                ProductID = Convert.ToInt32(dbreader["ProductID"]),
                                UnitPrice = Convert.ToDouble(dbreader["UnitPrice"]),
                                Quantity = Convert.ToInt32(dbreader["Quantity"]),
                                Discount = Convert.ToDouble(dbreader["Discount"])
                            };
                            data.Add(orderDetail);


                        }
                    }
                }
                connection.Close();
            }
            return data;
        }
    }
}
