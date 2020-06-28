using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class OrderDAL : IOrderDAL
    {
        private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Count(int page, int pageSize, string customerID, int employeeID, int shipperID)
        {
            int rowCount = 0;
            using (SqlConnection connection = new SqlConnection(connectionString)) //Tạo đối tượng kết nối CSDL
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())//Thực hiện truy vấn CSDL
                {
                    cmd.CommandText = @"
	                                        select count(*)
	                                        from Orders
	                                        where ((@customerID = N'') or (CustomerID like @customerID)) and
                                                  ((@employeeID = N'') or (EmployeeID like @employeeID)) and
                                                  ((@shipperID = N'') or (ShipperID like @shipperID))
                                        ";//Câu truy vấn
                    cmd.CommandType = System.Data.CommandType.Text; //Cho biết lệnh mà ta sd là lệnh gì
                    cmd.Connection = connection;//lệnh kết nối
                    cmd.Parameters.AddWithValue("@page", page);//Tham số truyền vào
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    cmd.Parameters.AddWithValue("@employeeID", employeeID);
                    cmd.Parameters.AddWithValue("@shipperID", shipperID);
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return rowCount;
        }

        public bool Delete(int[] orderIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Orders
                                            WHERE(OrderID = @orderID)
                                              AND(OrderID NOT IN(SELECT OrderID FROM OrderDetails))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@orderID", SqlDbType.Int);
                foreach (int orderID in orderIDs)
                {
                    cmd.Parameters["@orderID"].Value = orderID;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return result;
        }

        public List<Order> List(int page, int pageSize, string customerID, int employeeID, int shipperID)
        {
            List<Order> data = new List<Order>();
            using (SqlConnection connection = new SqlConnection(connectionString)) //Tạo đối tượng kết nối CSDL
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())//Thực hiện truy vấn CSDL
                {
                    cmd.CommandText = @"select *
                                        from
                                        (
	                                        select *,
	                                        ROW_NUMBER() over(order by OrderID) as RowNumber
	                                        from Orders
	                                        where ((@customerID = N'') or (CustomerID like @customerID)) and
                                                  ((@employeeID = N'') or (EmployeeID like @employeeID)) and
                                                  ((@shipperID = N'') or (ShipperID like @shipperID)) 
                                        ) as t
                                        where t.RowNumber between (@page-1)*@pageSize + 1 and @page*@pageSize
                                        order by t.RowNumber
                                        ";//Câu truy vấn
                    cmd.CommandType = System.Data.CommandType.Text; //Cho biết lệnh mà ta sd là lệnh gì
                    cmd.Connection = connection;//lệnh kết nối
                    cmd.Parameters.AddWithValue("@page", page);//Tham số truyền vào
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    cmd.Parameters.AddWithValue("@employeeID", employeeID);
                    cmd.Parameters.AddWithValue("@shipperID", shipperID);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            Order order = new Order()
                            {
                                OrderID = Convert.ToInt32(dbReader["OrderID"]),
                                CustomerID = Convert.ToString(dbReader["CustomerID"]),
                                EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                                OrderDate = Convert.ToDateTime(dbReader["OrderDate"]),
                                RequiredDate = Convert.ToDateTime(dbReader["RequiredDate"]),
                                ShippedDate = Convert.ToDateTime(dbReader["ShippedDate"]),
                                ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                                Freight = Convert.ToDouble(dbReader["Freight"]),
                                ShipAddress = Convert.ToString(dbReader["ShipAddress"]),
                                ShipCity = Convert.ToString(dbReader["ShipCity"]),
                                ShipCountry = Convert.ToString(dbReader["ShipCountry"])
                            };
                            data.Add(order);


                        }
                    }
                }
                connection.Close();
            }
            return data;
        }
    }
}
