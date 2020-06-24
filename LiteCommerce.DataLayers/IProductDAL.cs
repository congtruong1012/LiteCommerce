using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IProductDAL
    {
        /// <summary>
        /// Bổ sung thêm 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của supplier được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(Product data);
        /// <summary>
        /// Cập nhật 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true nêu cập nhật thành công và ngược lại</returns>
        bool Update(Product data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        bool Delete(int[] productIDs);
        Product Get(int productID);
        List<Product> List(int page, int pageSize, string searchValue, int category, int supplier);
        int count(string searchValue, int category, int supplier);
    }
}
