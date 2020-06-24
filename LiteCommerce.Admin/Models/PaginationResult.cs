using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class PaginationResult
    {
        public int Page { set; get; }
        public int PageSize { set; get; }
        public int RowCount { set; get; }
        public int PageCount
        {
            get
            {
                int pageCount = 1;
                pageCount = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    pageCount += 1;
                return pageCount;
            }
        }
        public string searchValue { set; get; }
    }
}