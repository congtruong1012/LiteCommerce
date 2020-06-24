using LiteCommerce.BusinessLayers;
using LiteCommerce.DataLayers.SQLServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> listRoles(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "-- All Roles --" });
            }
            list.Add(new SelectListItem() { Value = "anonymous", Text = "Anonymous" } );
            list.Add(new SelectListItem() { Value = "staff", Text = "Staff" });
            list.Add(new SelectListItem() { Value = "administrator", Text = "Administrator" });
            list.Add(new SelectListItem() { Value = "accountant", Text = "Accountant" });
            list.Add(new SelectListItem() { Value = "saleman", Text = "Saleman" });

            return list;
        }
        public static List<SelectListItem> listCountrys(bool allowSelectAll = true)
        {
            List<SelectListItem> listCountrys = new List<SelectListItem>();
            foreach(var country in CatalogBLL.Country_List())
            {
                listCountrys.Add(new SelectListItem() { Value = country.Name, Text = country.Name });
            }
            return listCountrys;
        }
        public static List<SelectListItem> Supplier(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "-- All Suppliers --" });
            }
            foreach (var supplier in CatalogBLL.Suppliers_List(1, CatalogBLL.Supplier_Count(""), "")){
                list.Add(new SelectListItem()
                {
                    Value = supplier.SupplierID.ToString(),
                    Text = supplier.CompanyName
                });
            }
            return list;
        }
        public static List<SelectListItem> Category(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "-- All Categories --" });
            }
            foreach (var category in CatalogBLL.Categorys_List(1, CatalogBLL.Category_Count(""), "")){
                list.Add(new SelectListItem()
                {
                    Value = category.CategoryID.ToString(),
                    Text = category.CategoryName
                });
            }
            return list;
        }
        public static List<SelectListItem> Customer(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "-- All Customers --" });
            }
            foreach (var customer in CatalogBLL.Customers_List(1, CatalogBLL.Customer_Count(""), ""))
            {
                list.Add(new SelectListItem()
                {
                    Value = customer.CustomerID,
                    Text = customer.CompanyName
                });
            }
            return list;
        }
        public static List<SelectListItem> Shiper(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "-- All Shippers --" });
            }
            foreach (var shipper in CatalogBLL.Shippers_List(1, CatalogBLL.Shipper_Count(""), ""))
            {
                list.Add(new SelectListItem()
                {
                    Value = shipper.ShipperID.ToString(),
                    Text = shipper.CompanyName
                });
            }
            return list;
        }
        public static List<SelectListItem> Employee(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "-- All Employees --" });
            }
            foreach (var employee in HumanResourceBLL.Employees_List(1, HumanResourceBLL.Employee_Count("",""), "",""))
            {
                list.Add(new SelectListItem()
                {
                    Value = employee.EmployeeID.ToString(),
                    Text = employee.LastName+" "+employee.FirstName
                });
            }
            return list;
        }
        public static List<SelectListItem> Product(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "-- All Products --" });
            }
            foreach (var product in CatalogBLL.Products_List(1, CatalogBLL.Product_Count("",0,0), "",0,0))
            {
                list.Add(new SelectListItem()
                {
                    Value = product.ProductID.ToString(),
                    Text = product.ProductName
                });
            }
            return list;
        }
    }
}