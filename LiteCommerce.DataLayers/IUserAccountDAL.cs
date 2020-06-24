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
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Kiểm tra thông tin có hợp lệ hay không?
        /// - Nếu hợp lệ thì trả về thông tin của user
        /// - Ngược lại trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);
        /// <summary>
        /// Lấy thông tin cá nhân
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        Employee GetProfile(string email);
        bool UpdateProfile(Employee data);
        bool ChangePassword(string email, string newPass);
        bool ChangeForgotPassword(string newPass);

    }
}
