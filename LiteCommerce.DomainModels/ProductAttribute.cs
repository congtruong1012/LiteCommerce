using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class ProductAttribute
    {
        public int AttributeID { set; get; }
        public int ProductID { set; get; }
        public string AttributeName { set; get; }
        public string AttributeValues { set; get; }
        public int DisplayOrder { set; get; }
    }
}
