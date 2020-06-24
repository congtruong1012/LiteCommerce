using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Bổ sung thêm 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của supplier được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(Supplier data);
        /// <summary>
        /// Cập nhật 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true nêu cập nhật thành công và ngược lại</returns>
        bool Update(Supplier data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        bool Delete(int[] supplierIDs);
        Supplier Get(int supplierID);
        List<Supplier> List(int page, int pageSize, string searchValue);
        int count(string searchValue);
    }
}
