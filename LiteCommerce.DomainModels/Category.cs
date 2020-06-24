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
    public class Category
    {
        public int CategoryID { set; get; }
        public string CategoryName { set; get; } 
        public string Description { set; get; }
    }
}
