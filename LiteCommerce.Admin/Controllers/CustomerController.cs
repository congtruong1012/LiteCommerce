using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class CustomerController : Controller
    {
        [AuthorizeRedirect(Roles = WebUserRoles.ACCOUNTANT)]
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.CustomerPaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Customer_Count(searchValue),
                Data = CatalogBLL.Customers_List(page, AppSettings.DefaultPageSize, searchValue),
                searchValue = searchValue
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add New Customer";
                Customer newCustomer = new Customer();
                newCustomer.CustomerID = "";
                ViewBag.method = "Add";
                return View(newCustomer);
            }
            else
            {
                ViewBag.Title = "Edit Customer";
                Customer editCustomer = CatalogBLL.Customer_Get(id);
                ViewBag.method = "Edit";
                if (editCustomer == null)
                {
                    return RedirectToAction("index");
                }
                return View(editCustomer);

            }
        }
        [HttpPost]
        public ActionResult Input(Customer model, string method)
        {
            try
            {

                if (String.Equals(method, "Add") == true)
                {
                    Customer customer = CatalogBLL.Customer_Get(model.CustomerID);
                    if (customer != null)
                    {
                        ModelState.AddModelError("id", "ID customer đã tồn tại");
                    }

                    if (String.IsNullOrEmpty(model.CompanyName))
                    {
                        ModelState.AddModelError("CompanyName", "Company Name is required");
                    }
                    if (String.IsNullOrEmpty(model.Country))
                    {
                        ModelState.AddModelError("errorAddr", "Vui lòng chọn quốc gia");
                    }
                    if (!ModelState.IsValid)
                    {
                        ViewBag.method = method;
                        ViewBag.CustomerID = model.CustomerID;
                        return View(model);
                    }
                    else
                    {
                        var addCustomer = CatalogBLL.Customer_Add(model);
                        return RedirectToAction("Index");
                    }
                }
                //if (String.Equals(method, "Edit") == true)
                else
                {
                    bool editCustomer = CatalogBLL.Customer_Update(model);
                    return RedirectToAction("Index");
                }



            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message + ":" + e.StackTrace);
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Delete(string method = "", string[] CustomerIDs = null)
        {
            if(CustomerIDs != null)
            {
                CatalogBLL.Customer_Delete(CustomerIDs);

            }
            return RedirectToAction("Index");
        }
    }
}