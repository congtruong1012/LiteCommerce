using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AuthorizeRedirect(Roles = WebUserRoles.ACCOUNTANT)]
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.CategoryPaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Category_Count(searchValue),
                Data = CatalogBLL.Categorys_List(page, AppSettings.DefaultPageSize, searchValue),
                searchValue = searchValue
            };
            return View(model);
        }
        
        public ActionResult Input(string id = "")
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add Category";
                Category newCategory = new Category();
                newCategory.CategoryID = 0;
                return View(newCategory);
            }
            else
            {
                ViewBag.Title = "Edit Category";
                Category editCategory = CatalogBLL.Category_Get(Convert.ToInt32(id));
                if (editCategory == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editCategory);
                }
            }
            
        }
        [HttpPost]
        public ActionResult Input(Category model)
        {
            try
            {
                //Kiểm tra tính hợp lệ
                if (string.IsNullOrEmpty(model.CategoryName))
                {
                    ModelState.AddModelError("Category_name", "City is required");
                }
                if (string.IsNullOrEmpty(model.Description))
                {
                    ModelState.AddModelError("desc", "Fax is required");
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.CategoryID == 0)
                {
                    int categoryID = CatalogBLL.Category_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Category_Update(model);
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
        public ActionResult Delete(string method, int[] CategoryIDs)
        {
            if (CategoryIDs != null)
            {
                bool result = CatalogBLL.Category_Delete(CategoryIDs);
            }
            return RedirectToAction("Index");
        }
    }
}