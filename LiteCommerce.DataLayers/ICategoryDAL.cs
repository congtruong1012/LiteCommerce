using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICategoryDAL
    {
        /// <summary>
        /// Bổ sung thêm 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của supplier được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(Category data);
        /// <summary>
        /// Cập nhật 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true nêu cập nhật thành công và ngược lại</returns>
        bool Update(Category data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>
        bool Delete(int[] categoryIDs);
        Category Get(int categoryID);
        List<Category> List(int page, int pageSize, string searchValue);
        int count(string searchValue);
    }
}
