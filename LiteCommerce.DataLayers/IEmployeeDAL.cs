using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Bổ sung thêm 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của supplier được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(Employee data);
        /// <summary>
        /// Cập nhật 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true nêu cập nhật thành công và ngược lại</returns>
        bool Update(Employee data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeIDs"></param>
        /// <returns></returns>
        bool Delete(int[] employeeIDs);
        Employee Get(int employeeID);
        List<Employee> List(int page, int pageSize, string searchValue, string country);
        int Count(string searchValue, string country);
        bool Check_Pass(string email, string password);
        Employee Information(string email);
        //int CheckPass(string pass);
        bool ChangePass(string email, string pass);
        bool CheckEmail(string email);
        bool UpdateImage(string employeeID, string image);
    }
}
