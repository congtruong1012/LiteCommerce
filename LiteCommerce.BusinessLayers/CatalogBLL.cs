using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// 
    /// </summary>
    public static class CatalogBLL
    {
        /// <summary>
        /// 
        /// </summary>
        private static ISupplierDAL SupplierDB { get; set; }
        private static IShipperDAL ShipperDB { get; set; }
        private static ICustomerDAL CustomerDB { get; set; }
        private static ICategoryDAL CategoryDB { get; set; }
        private static IProductDAL ProductDB { get; set; }
        private static ICountryDAL CountryDB { get; set; }
        private static IProductAttributeDAL productAttributeDB { get; set; }
        /// <summary>
        /// Hàm này phải được gọi để khởi tạo các chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            SupplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            ShipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            CustomerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            CategoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
            ProductDB = new DataLayers.SQLServer.ProductDAL(connectionString);
            CountryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
            productAttributeDB = new DataLayers.SQLServer.ProductAttributeDAL(connectionString);
        }
        /// <summary>
        /// Danh sách supplier
        /// </summary>
        /// <param name="page">trang</param>
        /// <param name="pageSize">tổng số trang</param>
        /// <param name="searchValue">giá trị tìm kiếm</param>
        /// <returns></returns>
        public static List<Supplier> Suppliers_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30; 
            return SupplierDB.List(page, pageSize, searchValue);
        }
        public static int Supplier_Count(string searchValue)
        {
            return SupplierDB.count(searchValue);
        }
        /// <summary>
        /// Bổ sung 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Supplier_Add(Supplier data)
        {
            return SupplierDB.Add(data);
        }
        /// <summary>
        /// Lấy 1 supplier
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier Supplier_Get(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }
        /// <summary>
        /// Cập nhập 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Supplier_Update(Supplier data)
        {
            return SupplierDB.Update(data);
        }
        /// <summary>
        /// Xóa 1 supplier
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        public static bool Supplier_Delete(int[] supplierIDs)
        {
            return SupplierDB.Delete(supplierIDs);
        }
        /// <summary>
        /// Danh sách shipper
        /// </summary>
        /// <param name="page">trang</param>
        /// <param name="pageSize">tổng số trang</param>
        /// <param name="searchValue">giá trị tìm kiếm</param>
        /// <returns></returns>
        public static List<Shipper> Shippers_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return ShipperDB.List(page, pageSize, searchValue);
        }
        public static int Shipper_Count(string searchValue)
        {
            return ShipperDB.count(searchValue);
        }
        /// <summary>
        /// Bổ sung 1 shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Shipper_Add(Shipper data)
        {
            return ShipperDB.Add(data);
        }
        /// <summary>
        /// Lấy 1 shipper
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper Shipper_Get(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }
        /// <summary>
        /// Cập nhập 1 shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Shipper_Update(Shipper data)
        {
            return ShipperDB.Update(data);
        }
        /// <summary>
        /// Xóa 1 shipper
        /// </summary>
        /// <param name="shipperIDs"></param>
        /// <returns></returns>
        public static bool Shipper_Delete(int[] shipperIDs)
        {
            return ShipperDB.Delete(shipperIDs);
        }
        /// <summary>
        /// Danh sách product
        /// </summary>
        /// <param name="page">trang</param>
        /// <param name="pageSize">tổng số trang</param>
        /// <param name="searchValue">giá trị tìm kiếm</param>
        /// <returns></returns>
        public static List<Product> Products_List(int page, int pageSize, string searchValue, int category, int supplier)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return ProductDB.List(page, pageSize, searchValue, category, supplier);
        }
        public static int Product_Count(string searchValue, int category, int supplier)
        {
            return ProductDB.count(searchValue,category,supplier);
        }
        public static int Product_Add(Product data)
        {
            return ProductDB.Add(data);
        }
        public static Product Product_Get(int productID)
        {
            return ProductDB.Get(productID);
        }
        public static bool Product_Update(Product data) {
            return ProductDB.Update(data);
        }
        public static bool Product_Delete(int[] productIDs)
        {
            return ProductDB.Delete(productIDs);
        }
        /// <summary>
        /// Danh sách customer
        /// </summary>
        /// <param name="page">trang</param>
        /// <param name="pageSize">tổng số trang</param>
        /// <param name="searchValue">giá trị tìm kiếm</param>
        /// <returns></returns>
        public static List<Customer> Customers_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return CustomerDB.List(page, pageSize, searchValue);
        }
        public static int Customer_Count(string searchValue)
        {
            return CustomerDB.count(searchValue);
        }
        /// <summary>
        /// Bổ sung 1 customer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Customer_Add(Customer data)
        {
            return CustomerDB.Add(data);
        }
        /// <summary>
        /// Lấy 1 Customer
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Customer Customer_Get(string customerID)
        {
            return CustomerDB.Get(customerID);
        }
        /// <summary>
        /// Cập nhập 1 Customer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Customer_Update(Customer data)
        {
            return CustomerDB.Update(data);
        }
        /// <summary>
        /// Xóa 1 Customer
        /// </summary>
        /// <param name="CustomerIDs"></param>
        /// <returns></returns>
        public static bool Customer_Delete(string[] customerIDs)
        {
            return CustomerDB.Delete(customerIDs);
        }
        /// <summary>
        /// Danh sách category
        /// </summary>
        /// <param name="page">trang</param>
        /// <param name="pageSize">tổng số trang</param>
        /// <param name="searchValue">giá trị tìm kiếm</param>
        /// <returns></returns>
        public static List<Category> Categorys_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return CategoryDB.List(page, pageSize, searchValue);
        }
        public static int Category_Count(string searchValue)
        {
            return CategoryDB.count(searchValue);
        }
        /// <summary>
        /// Bổ sung 1 category
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Category_Add(Category data)
        {
            return CategoryDB.Add(data);
        }
        /// <summary>
        /// Lấy 1 Category
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static Category Category_Get(int categoryID)
        {
            return CategoryDB.Get(categoryID);
        }
        /// <summary>
        /// Cập nhập 1 Category
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Category_Update(Category data)
        {
            return CategoryDB.Update(data);
        }
        /// <summary>
        /// Xóa 1 Category
        /// </summary>
        /// <param name="CategoryIDs"></param>
        /// <returns></returns>
        public static bool Category_Delete(int[] categoryIDs)
        {
            return CategoryDB.Delete(categoryIDs);
        }
        public static List<Country> Country_List()
        {
            return CountryDB.list();
        }
        public static List<ProductAttribute> ProductAttribute_List(int productID)
        {
            return productAttributeDB.List(productID);
        }
        public static int ProductAttribute_Add(ProductAttribute data)
        {
            return productAttributeDB.Add(data);
        }
        public static ProductAttribute ProductAttribute_Get(int attributeID)
        {
            return productAttributeDB.Get(attributeID);
        }
        public static bool ProductAttribute_Update(ProductAttribute data)
        {
            return productAttributeDB.Update(data);
        }
        public static bool ProductAttribute_Delete(int[] atrributeIDs)
        {
            return productAttributeDB.Delete(atrributeIDs);
        }
    }
}
