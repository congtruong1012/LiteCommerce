using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        [AuthorizeRedirect(Roles = WebUserRoles.ADMINISTRATOR)]
        public ActionResult Index(int page = 1, string searchValue = "", string country = "")
        {
            WebUserData userData = User.GetUserData();
            var model = new Models.EmployeePaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = HumanResourceBLL.Employee_Count(searchValue,country),
                Data = HumanResourceBLL.Employees_List(page, AppSettings.DefaultPageSize, searchValue, country),
                searchValue = searchValue,
                Country = country

            };
            return View(model);
            
        }

        public ActionResult Input(string id = "")
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add Employee";
                Employee newEmployee = new Employee();
                newEmployee.EmployeeID = 0;
                return View(newEmployee);
            }
            else
            {
                ViewBag.Title = "Edit Employee";
                Employee editEmployee = HumanResourceBLL.Employee_Get(Convert.ToInt32(id));
                if(editEmployee == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editEmployee);
            }
        }
        [HttpPost]
        public ActionResult Input(Employee model, HttpPostedFileBase file = null, string oldEmail = "")
        {
            //try
            //{
            //    //Kiểm tra tính hợp lệ
            if(model.Notes == null)
            {
                model.Notes = "";
            }
            if(String.IsNullOrEmpty(model.Country))
            {
                ModelState.AddModelError("errorAddr", "Vui lòng chọn quốc gia");
            }
            if (String.IsNullOrEmpty(model.GroupName))
            {
                ModelState.AddModelError("errorRole", "Vui lòng chọn quyền");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var listRoles = model.GroupName.Split(',');
            string groupName = "";
            foreach(var item in SelectListHelper.listRoles(false))
            {
                foreach(var role in listRoles)
                {
                    if(item.Value == role)
                    {
                        groupName += role + ",";
                    }
                }
            }
            model.GroupName = groupName.Remove(groupName.LastIndexOf(','));
            if (file != null)
            {
                string get = DateTime.Now.ToString("ddMMyyyhhmmss");
                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = get + fileExtension;
                string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                model.PhotoPath = fileName;
                file.SaveAs(path);
            }
            if (!EncodeMD5.IsMD5(model.Password))
            {
                model.Password = EncodeMD5.GetMD5(model.Password);
            }
            if (model.EmployeeID == 0)
            {
                if (file == null)
                {
                    TempData["emptyFile"] = "Vui lòng chọn file";
                    return View(model);
                }
                else if (HumanResourceBLL.Check_Email(model.Email))
                {
                    TempData["emptyEmail"] = "Email đã tồn tại";
                    return View(model);
                }
                else
                {
                    int employeeID = HumanResourceBLL.Employee_Add(model);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if(HumanResourceBLL.Check_Email(model.Email)&&(oldEmail != model.Email)){
                    TempData["emptyEmail"] = "Email đã tồn tại";
                    ViewBag.oldEmail = oldEmail;
                    return View(model);
                }
                var getEmployee = HumanResourceBLL.Employee_Get(model.EmployeeID);
                if (file == null)
                {
                    model.PhotoPath = getEmployee.PhotoPath;
                }

                bool updateResult = HumanResourceBLL.Employee_Update(model);
                return RedirectToAction("Index");
            }

            //}
            //catch (Exception e)
            //{
            //    ModelState.AddModelError("", e.Message + ":" + e.StackTrace);
            //    return View(model);
            //}
        }
        [HttpPost]
        public ActionResult Delete(string method, int[] EmployeeIDs)
        {
            if (EmployeeIDs!= null)
            {
                bool delete = HumanResourceBLL.Employee_Delete(EmployeeIDs);
            }
            return RedirectToAction("Index");
        }
    }
}