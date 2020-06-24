using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Employee
    {
        public int EmployeeID { set; get; }
        public string LastName { set; get; }
        public string FirstName { set; get; }
        public string Title { set; get; }
	    public DateTime BirthDate { set; get; }
	    public DateTime HireDate { set; get; }
	    public string Email { set; get; }
	    public string Address { set; get; }
	    public string City { set; get; }
	    public string Country { set; get; }
	    public string HomePhone { set; get; }
	    public string Notes { set; get; }
	    public string PhotoPath { set; get; }
	    public string Password { set; get; }
        public string GroupName { set; get; }
    }
}
