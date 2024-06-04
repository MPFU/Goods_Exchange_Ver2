using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.FilterModel.Helper
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItem { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
    }
}
