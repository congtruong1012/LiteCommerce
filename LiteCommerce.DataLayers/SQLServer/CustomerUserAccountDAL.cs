using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class CustomerUserAccountDAL: IUserAccountDAL
    {
        private string connectionString;
        public CustomerUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Authorize khách hàng
        /// </summary>
        /// <param name="userName">Địa chỉ email của khách hàng</param>
        /// <param name="password">Mật khẩu(MD5)</param>
        /// <returns></returns>
        public UserAccount Authorize(string userName, string password)
        {
            UserAccount userAccount = new UserAccount()
            {
                UserID = userName,
                FullName = "Nguyễn Văn Phúc Phôn",
                Photo = "photo1.png"
            };
            return userAccount;
        }

        public bool ChangeForgotPassword(string newPass)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string oldPass, string newPass, string confirPass)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string email, string newPass)
        {
            throw new NotImplementedException();
        }

        public Employee GetProfile(string email)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProfile(Employee data)
        {
            throw new NotImplementedException();
        }
    }
}
