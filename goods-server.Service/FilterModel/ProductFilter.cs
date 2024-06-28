using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.FilterModel
{
    public class ProductFilter : FilterOption<Product>
    {
        public string? Title { get; set; }
        public Guid? CreatorId { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
        public int? Rated { get; set; }
        public string? CityName { get; set; }
        public string? GenreName { get; set; }
        public string? Status { get; set; }
        public string? IsDisplay {  get; set; }
    }
}
