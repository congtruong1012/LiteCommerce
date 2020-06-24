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
    public class ProductDAL : IProductDAL
    {
        private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ProductDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Add(Product data)
        {
            int productID = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Products
                                          (
	                                          ProductName,
	                                          SupplierID,
	                                          CategoryID,
	                                          QuantityPerUnit,
	                                          UnitPrice,
                                              Descriptions,
                                              PhotoPath
                                          )
                                          VALUES
                                          (
	                                          @productName,
	                                          @supplierID,
	                                          @categoryID,
	                                          @quantityPerUnit,
	                                          @unitPrice,
                                              @descriptions,
                                              @photoPath
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@productName", data.ProductName);
                cmd.Parameters.AddWithValue("@supplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@categoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@quantityPerUnit", data.QuantityPerUnit);
                cmd.Parameters.AddWithValue("@unitPrice", data.UnitPrice);
                cmd.Parameters.AddWithValue("@descriptions", data.Descriptions);
                cmd.Parameters.AddWithValue("@photoPath", data.PhotoPath);

                productID = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return productID;
        }

        public int count(string searchValue, int category, int supplier)
        {
            int rowCount = 0;
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            using (SqlConnection connection = new SqlConnection(connectionString)) //Tạo đối tượng kết nối CSDL
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())//Thực hiện truy vấn CSDL
                {
                    cmd.CommandText = @"select COUNT(*) from Products where ((@searchValue = N'') or (ProductName like @searchValue) )and ((@category = N'') or (CategoryID = @category)) and
												  ((@supplier = N'') or (SupplierID = @supplier))
                                        ";//Câu truy vấn
                    cmd.CommandType = System.Data.CommandType.Text; //Cho biết lệnh mà ta sd là lệnh gì
                    cmd.Connection = connection;//lệnh kết nối
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@supplier", supplier);
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return rowCount;
        }

        public bool Delete(int[] productIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Products
                                            WHERE ProductID = @productID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@productID", SqlDbType.Int);
                foreach (var productID in productIDs)
                {
                    cmd.Parameters["@productID"].Value = productID;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return result;
        }

        public Product Get(int productID)
        {
            Product product = null;
            using (SqlConnection connection = new SqlConnection(connectionString)) //Tạo đối tượng kết nối CSDL
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())//Thực hiện truy vấn CSDL
                {
                    cmd.CommandText = @"select *
                                       from Products where ProductID = @productID
                                        ";//Câu truy vấn
                    cmd.CommandType = System.Data.CommandType.Text; //Cho biết lệnh mà ta sd là lệnh gì
                    cmd.Connection = connection;//lệnh kết nối
                    cmd.Parameters.AddWithValue("@productID", productID);
                    using (SqlDataReader dbreader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (dbreader.Read())
                        {
                            product = new Product();
                            product.ProductID = Convert.ToInt32(dbreader["ProductID"]);
                            product.ProductName = Convert.ToString(dbreader["ProductName"]);
                            product.SupplierID = Convert.ToInt32(dbreader["SupplierID"]);
                            product.CategoryID = Convert.ToInt32(dbreader["CategoryID"]);
                            product.QuantityPerUnit = Convert.ToString(dbreader["QuantityPerUnit"]);
                            product.UnitPrice = Convert.ToDouble(dbreader["UnitPrice"]);
                            product.Descriptions = Convert.ToString(dbreader["Descriptions"]);
                            product.PhotoPath = Convert.ToString(dbreader["PhotoPath"]);


                        }
                    }
                }
                connection.Close();
            }
            return product;
        }

        public List<Product> List(int page, int pageSize, string searchValue, int category, int supplier)
        {
                List<Product> data = new List<Product>();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = "%" + searchValue + "%";
                }
                using (SqlConnection connection = new SqlConnection(connectionString)) //Tạo đối tượng kết nối CSDL
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand())//Thực hiện truy vấn CSDL
                    {
                        cmd.CommandText = @"select *
                                            from
                                            (
	                                            select *,
	                                            ROW_NUMBER() over(order by ProductID) as RowNumber
	                                            from Products
	                                            where ((@searchValue = N'') or(ProductName like @searchValue)) and
												      ((@category = N'') or (CategoryID = @category)) and
												      ((@supplier = N'') or (SupplierID = @supplier))
                                            ) as t
                                            where t.RowNumber between (@page-1)*@pageSize + 1 and @page*@pageSize
                                            order by t.RowNumber
                                            ";//Câu truy vấn
                        cmd.CommandType = System.Data.CommandType.Text; //Cho biết lệnh mà ta sd là lệnh gì
                        cmd.Connection = connection;//lệnh kết nối
                        cmd.Parameters.AddWithValue("@page", page);//Tham số truyền vào
                        cmd.Parameters.AddWithValue("@pageSize", pageSize);
                        cmd.Parameters.AddWithValue("@searchValue", searchValue);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@supplier", supplier);
                        using (SqlDataReader dbreader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (dbreader.Read())
                            {
                                Product product = new Product();
                                product.ProductID = Convert.ToInt32(dbreader["ProductID"]);
                                product.ProductName = Convert.ToString(dbreader["ProductName"]);
                                product.SupplierID = Convert.ToInt32(dbreader["SupplierID"]);
                                product.CategoryID = Convert.ToInt32(dbreader["CategoryID"]);
                                product.QuantityPerUnit = Convert.ToString(dbreader["QuantityPerUnit"]);
                                product.UnitPrice = Convert.ToDouble(dbreader["UnitPrice"]);
                                product.Descriptions = Convert.ToString(dbreader["Descriptions"]);
                                product.PhotoPath = Convert.ToString(dbreader["PhotoPath"]);
                                data.Add(product);
                            

                            }
                        }
                    }
                    connection.Close();
                }
                return data;
        }

        public bool Update(Product data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Products
                                  SET   
                                    ProductName = @productName,
	                                SupplierID = @supplierID,
	                                CategoryID = @categoryID,
                                    QuantityPerUnit = @quantityPerUnit,
                                    UnitPrice = @unitPrice,
                                    Descriptions = @descriptions,
	                                PhotoPath = @photoPath
                                  WHERE ProductID = @productID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@productName", data.ProductName);
                cmd.Parameters.AddWithValue("@supplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@categoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@quantityPerUnit", data.QuantityPerUnit);
                cmd.Parameters.AddWithValue("@unitPrice", data.UnitPrice);
                cmd.Parameters.AddWithValue("@descriptions", data.Descriptions);
                cmd.Parameters.AddWithValue("@photoPath", data.PhotoPath);
                cmd.Parameters.AddWithValue("@productID", data.ProductID);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
