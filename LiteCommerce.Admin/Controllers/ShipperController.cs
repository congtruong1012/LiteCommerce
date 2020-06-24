using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class ShipperController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [AuthorizeRedirect(Roles = WebUserRoles.ACCOUNTANT)]
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.ShipperPaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Shipper_Count(searchValue),
                Data = CatalogBLL.Shippers_List(page, AppSettings.DefaultPageSize, searchValue),
                searchValue = searchValue
            };
            return View(model);
        }

        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add New Shipper";
                Shipper newShipper = new Shipper();
                newShipper.ShipperID = 0;
                return View(newShipper);
            }
            else
            {
                ViewBag.Title = "Edit Shipper";
                Shipper editShipper = CatalogBLL.Shipper_Get(Convert.ToInt32(id));
                if (editShipper == null)
                {
                    return RedirectToAction("index");
                }
                return View(editShipper);

            }
        }
        [HttpPost]
        public ActionResult Shipper_Controller(FormCollection collection)
        {
            Shipper data = new Shipper();
            data.ShipperID = Convert.ToInt32(Request.Form["shipperID"]);
            data.CompanyName = Request.Form["companyName"];
            data.Phone = Request.Form["phone"];
            
            if (data.ShipperID == 0)
            {
                CatalogBLL.Shipper_Add(data);
            }
            else
            {
                CatalogBLL.Shipper_Update(data);
            }
            return RedirectToAction("Index", "Shipper");

        }
        [HttpPost]
        public ActionResult Delete(string method, int[] ShipperIDs)
        {
            if (ShipperIDs != null)
            {
                bool result = CatalogBLL.Shipper_Delete(ShipperIDs);
            }
            return RedirectToAction("Index");
        }

    }
}