using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SQLServer;
using LiteCommerce.DomainModels;

namespace LiteCommerce.BusinessLayers
{
    public static class AccountBLL
    {
        private static IEmployeeDAL EmployeeDB { get; set; }
        private static string connectionString;
        public static void Initialize(string connectionString)
        {
            EmployeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            AccountBLL.connectionString = connectionString;
        }
        public static UserAccount Athorize(string username, string password, UserAccountTypes userType)
        {
            IUserAccountDAL UserAccountDB = null;
            switch (userType)
            {
                case UserAccountTypes.Employee:
                    UserAccountDB = new EmployeeUserAccountDAL(connectionString);
                    break;
                case UserAccountTypes.Customer:
                    UserAccountDB = new CustomerUserAccountDAL(connectionString);
                    break;
                default:
                    throw new Exception("Usertype is not valid");
            }
            return UserAccountDB.Authorize(username, password);
        }
        public static Employee GetProfile(string email)
        {
            IUserAccountDAL UserAccountDB = new EmployeeUserAccountDAL(connectionString);
            return UserAccountDB.GetProfile(email);   
        }
        public static bool UpdateProfile(Employee data)
        {
            IUserAccountDAL UserAccountDB = new EmployeeUserAccountDAL(connectionString);
            return UserAccountDB.UpdateProfile(data);
        }
        public static bool Check_Pass(string email, string pass)
        {
            return EmployeeDB.Check_Pass(email,pass);
        }
        public static bool Change_Pass(string email, string pass)
        {
            IUserAccountDAL UserAccountDB = new EmployeeUserAccountDAL(connectionString);
            return UserAccountDB.ChangePassword(email, pass);
        }
        public static bool CheckEmail(string email)
        {
            return EmployeeDB.CheckEmail(email);
        }
    }
}
