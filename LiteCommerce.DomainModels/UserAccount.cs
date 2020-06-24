using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class UserAccount
    {
        /// <summary>
        /// ID của User
        /// </summary>
        public string UserID { set; get; }
        /// <summary>
        /// Tên
        /// </summary>
        public string FullName { set; get; }
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string Photo { set; get; }
        /// <summary>
        /// Phân quyền
        /// </summary>
        public string Role { set; get; }
    }
}
