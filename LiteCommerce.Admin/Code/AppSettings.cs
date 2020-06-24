using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    public static class AppSettings
    {
        public static int DefaultPageSize
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPageSize"]);
            }
        }
        public static string DefaultDateTime
        {
            get
            {
                return DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
    }
}