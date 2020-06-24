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
    public class ProductAttributeDAL : IProductAttributeDAL
    {
        private string connectionString;
        public ProductAttributeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Add(ProductAttribute data)
        {
            int attributeId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes
                                          (
	                                          ProductID,
	                                          AttributeName,
                                              AttributeValues,
                                              DisplayOrder
                                          )
                                          VALUES
                                          ( 
                                              @productID,
	                                          @attributeName,
                                              @attributeValues,
                                              @displayOrder
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@productID", data.ProductID);
                cmd.Parameters.AddWithValue("@attributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@attributeValues", data.AttributeValues);
                cmd.Parameters.AddWithValue("@displayOrder", data.DisplayOrder);

                attributeId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return attributeId;
        }

        public int CheckEmpty(int productID)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int[] attributeIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes
                                            WHERE AttributeID = @attributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@attributeID", SqlDbType.Int);
                foreach (int attributeID in attributeIDs)
                {
                    cmd.Parameters["@attributeID"].Value = attributeID;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return result;
        }

        public ProductAttribute Get(int attributeID)
        {
            ProductAttribute data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM ProductAttributes WHERE AttributeID = @attributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@categoryID", attributeID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValues = Convert.ToString(dbReader["AttributeValues"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<ProductAttribute> List(int productID)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection connection = new SqlConnection(connectionString)) //Tạo đối tượng kết nối CSDL
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())//Thực hiện truy vấn CSDL
                {
                    cmd.CommandText = @"select * from ProductAttributes where ProductID = @productID Order By DisplayOrder
                                        ";//Câu truy vấn
                    cmd.CommandType = System.Data.CommandType.Text; //Cho biết lệnh mà ta sd là lệnh gì
                    cmd.Connection = connection;//lệnh kết nối
                    cmd.Parameters.AddWithValue("@productID", productID);
                    using (SqlDataReader dbreader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (dbreader.Read())
                        {
                            ProductAttribute productAttribute = new ProductAttribute();
                            productAttribute.AttributeID = Convert.ToInt32(dbreader["AttributeID"]);
                            productAttribute.ProductID = Convert.ToInt32(dbreader["ProductID"]);
                            productAttribute.AttributeName = Convert.ToString(dbreader["AttributeName"]);
                            productAttribute.AttributeValues = Convert.ToString(dbreader["AttributeValues"]);
                            productAttribute.DisplayOrder = Convert.ToInt32(dbreader["DisplayOrder"]);
                            data.Add(productAttribute);


                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(ProductAttribute data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE ProductAttributes
                                  SET
                                    ProductID = @productID,
	                                AttributeName = @attributeName,
                                     AttributeValues = @attributeValues,
                                     DisplayOrder = @displayOrder
                                  WHERE AttributeID = @attributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@attributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@attributeValues", data.AttributeValues);
                cmd.Parameters.AddWithValue("@displayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@attributeID", data.AttributeID);
                cmd.Parameters.AddWithValue("@productID ", data.ProductID);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
