using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class HumanResourceBLL
    {
        /// <summary>
        /// 
        /// </summary>
        private static IEmployeeDAL EmployeeDB { get; set; }
        /// <summary>
        /// Hàm này phải được gọi để khởi tạo các chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            EmployeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
        }
        /// <summary>
        /// Danh sách Employee
        /// </summary>
        /// <param name="page">trang</param>
        /// <param name="pageSize">tổng số trang</param>
        /// <param name="searchValue">giá trị tìm kiếm</param>
        /// <returns></returns>
        public static List<Employee> Employees_List(int page, int pageSize, string searchValue, string country)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return EmployeeDB.List(page, pageSize, searchValue, country);
        }
        public static int Employee_Count(string searchValue, string country)
        {
            return EmployeeDB.Count(searchValue,country);
        }
        /// <summary>
        /// Bổ sung 1 Employee
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Employee_Add(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        /// <summary>
        /// Lấy 1 Employee
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static Employee Employee_Get(int employeeID)
        {
            return EmployeeDB.Get(employeeID);
        }
        /// <summary>
        /// Cập nhập 1 Employee
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Employee_Update(Employee data)
        {
            return EmployeeDB.Update(data);
        }
        /// <summary>
        /// Xóa 1 Employee
        /// </summary>
        /// <param name="EmployeeIDs"></param>
        /// <returns></returns>
        public static bool Employee_Delete(int[] employeeIDs)
        {
            return EmployeeDB.Delete(employeeIDs);
        }
        public static bool Check_Email(string email)
        {
            return EmployeeDB.CheckEmail(email);
        }
    }
}
