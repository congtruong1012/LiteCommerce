using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class SupplierController : Controller
    {
        /// <summary>
        /// Trang chủ 
        /// </summary>
        /// <returns></returns>
        /// 
        [AuthorizeRedirect(Roles = WebUserRoles.ACCOUNTANT)]
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.SupplierPaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Supplier_Count(searchValue),
                Data = CatalogBLL.Suppliers_List(page, AppSettings.DefaultPageSize, searchValue),
                searchValue = searchValue
            };
           
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add New Supplier";
                Supplier newSupplier = new Supplier();
                newSupplier.SupplierID = 0;
                return View(newSupplier);
            }
            else
            {
                ViewBag.Title = "Edit Supplier";
                Supplier editSupplier = CatalogBLL.Supplier_Get(Convert.ToInt32(id));
                if(editSupplier == null)
                {
                    return RedirectToAction("index");
                }
                return View(editSupplier);

            }
        }

        [HttpPost]
        public ActionResult Input(Supplier model)
        {
            try
            {
                //Kiểm tra tính hợp lệ
                if (string.IsNullOrEmpty(model.City))
                {
                    ModelState.AddModelError("City", "City is required");
                }
                if (string.IsNullOrEmpty(model.Fax))
                {
                    ModelState.AddModelError("Fax", "Fax is required");
                }
                if (String.IsNullOrEmpty(model.Country))
                {
                    ModelState.AddModelError("errorAddr", "Vui lòng chọn quốc gia");
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.SupplierID == 0)
                {
                    int supplierID = CatalogBLL.Supplier_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Supplier_Update(model);
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message + ":" + e.StackTrace);
                return View(model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string method = "", int[] supplierIDs = null)
        {
            if(supplierIDs != null)
            {
                CatalogBLL.Supplier_Delete(supplierIDs);

            }
            return RedirectToAction("Index");
        }
    }
}