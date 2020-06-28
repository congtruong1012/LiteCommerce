using LiteCommerce.Admin.Models;
using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class OrderController : Controller
    {
	//Hiển thị danh sách hóa đơn
        [AuthorizeRedirect(Roles = WebUserRoles.SALEMAN)] 
        public ActionResult Index(int page = 1, string customerID = "", int employeeID = 0, int shipperID = 0)
        {
            var model = new OrderPaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = ReportBLL.Order_Count(page, AppSettings.DefaultPageSize, customerID, employeeID, shipperID),
                Data = ReportBLL.Order_List(page, AppSettings.DefaultPageSize, customerID, employeeID, shipperID),
                customerID = customerID,
                employeeID = employeeID,
                shipperID = shipperID
            };
            return View(model);
        }

        public ActionResult CreateNew()
        {
            return View();
        }
        public ActionResult OrderDetail(string id = "")
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            var model = ReportBLL.OrderDetail_List(Convert.ToInt32(id));
            if (model.Count != 0)
            {
                ViewData["OrderDetail"] = model;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
	//Xóa đơn hàng
        [HttpPost]
        public ActionResult Delete(int[] orderIDs)
        {
            if (orderIDs != null)
            {
                var rs = ReportBLL.Order_Delete(orderIDs); 
            }
            return RedirectToAction("Index");
        }
    }
}