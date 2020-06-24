using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// Khơi tạo các chức năng tác nghiệp cho ứng dụng
    /// </summary>
    public static class BusinessLayerConfig
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public static void Initialize()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["LiteCommerce"].ConnectionString;
            CatalogBLL.Initialize(connectionString);
            HumanResourceBLL.Initialize(connectionString);
            AccountBLL.Initialize(connectionString);
            ReportBLL.Initialize(connectionString);
            //TODO: Bổ sung khởi tạo các BLL khác khi cần sử dụng
        }
    }
}