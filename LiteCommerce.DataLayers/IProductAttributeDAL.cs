using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IProductAttributeDAL
    {
        List<ProductAttribute> List(int productID);
        int Add(ProductAttribute data);
        ProductAttribute Get(int attributeID);
        bool Update(ProductAttribute data);
        bool Delete(int[] attributeIDs);
    }
}
