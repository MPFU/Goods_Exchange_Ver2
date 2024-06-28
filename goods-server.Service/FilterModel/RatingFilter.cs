using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;

namespace goods_server.Service.FilterModel
{
    public class RatingFilter : FilterOption<Rating>
    {
        public Guid? ProductId { get; set; }
    }
}
