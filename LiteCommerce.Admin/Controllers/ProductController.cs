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
    public class ProductController : Controller
    {
        [AuthorizeRedirect(Roles = WebUserRoles.ACCOUNTANT)]
        public ActionResult Index(int page = 1, string searchValue = "", int category = 0, int supplier = 0)
        {
            var model = new Models.ProductPaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Product_Count(searchValue,category,supplier),
                Data = CatalogBLL.Products_List(page, AppSettings.DefaultPageSize, searchValue,category, supplier),
                Category = category,
                Supplier = supplier
            };
            ViewData["Category"] = CatalogBLL.Categorys_List(1, CatalogBLL.Category_Count(""), "");
            ViewData["Supplier"] = CatalogBLL.Suppliers_List(1, CatalogBLL.Supplier_Count(""), "");
            return View(model);
        }
        public ActionResult Detail(string id="")
        {
            if (!String.IsNullOrEmpty(id))
            {
                Product model = CatalogBLL.Product_Get(Convert.ToInt32(id));
                if (model == null)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ViewData["Attribute"] = CatalogBLL.ProductAttribute_List(model.ProductID);
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult Input(string id = "")
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add Product";
                var newProduct = new Product();
                newProduct.ProductID = 0;
                return View(newProduct);
            }
            else
            {
                ViewBag.Title = "Edit Product";
                var editProduct = CatalogBLL.Product_Get(Convert.ToInt32(id));
                if (editProduct == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Attribute"] = CatalogBLL.ProductAttribute_List(editProduct.ProductID);
                    return View(editProduct);
                }
            }
            
        }
        [HttpPost]
        public ActionResult Input(Product model, HttpPostedFileBase file = null)
        {
            //try
            //{
            //    //Kiểm tra tính hợp lệ
            if (model.Descriptions == null)
            {
                model.Descriptions = "";
            }
            if(file != null)
            {
                string get = DateTime.Now.ToString("ddMMyyyhhmmss");
                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = get + fileExtension;
                string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                model.PhotoPath = fileName;
                file.SaveAs(path);
            }
            if (model.ProductID == 0)
            {
                if (file == null)
                {
                    TempData["emptyFile"] = "Vui lòng chọn file";
                    return RedirectToAction("Input");
                }
                else
                {
                    int productID = CatalogBLL.Product_Add(model);
                    //return Content( productID.ToString());
                    TempData["productID"] = productID;
                    return RedirectToAction("Add_Attr");
                }
                
            }
            else
            {
                
                var getProduct = CatalogBLL.Product_Get(model.ProductID);
                if (file == null)
                {
                    model.PhotoPath = getProduct.PhotoPath;
                }
                 
                bool updateResult = CatalogBLL.Product_Update(model);
                return RedirectToAction("Index");
            }
            
            //}
            //catch (Exception e)
            //{
            //    ModelState.AddModelError("", e.Message + ":" + e.StackTrace);
            //    return View(model);
            //}
        }
        public ActionResult Delete(int[] ProductIDs)
        {
            if (ProductIDs != null)
            {
                var deleteProduct = CatalogBLL.Product_Delete(ProductIDs);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Add_Attr(string id = "")
        {
            return View();
        }
        [HttpPost]
        public ActionResult Input_Attr(ProductAttribute model)
        {
            if (model.AttributeID == 0)
            {
                var addAttr = CatalogBLL.ProductAttribute_Add(model);
                return RedirectToAction("Input", "Product", new { id = model.ProductID });

                //return Content(addAttr.ToString());
            }
            else
            {
                var editAttr = CatalogBLL.ProductAttribute_Update(model);
                return RedirectToAction("Input", "Product", new { id = model.ProductID });

                //return Content(editAttr.ToString());
            }
        }
        [HttpPost]
        public ActionResult Delete_Attr(int[] attributes, string productID)
        {
            if(attributes != null)
            {
                var deleteAttr = CatalogBLL.ProductAttribute_Delete(attributes);
            }
            return RedirectToAction("Input", "Product", new { id = Convert.ToInt32(productID) });
        }
    }
}