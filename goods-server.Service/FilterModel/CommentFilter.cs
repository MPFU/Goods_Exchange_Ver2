using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;

namespace goods_server.Service.FilterModel
{
    public class CommentFilter : FilterOption<Comment>
    {
        public Guid ProductId { get; set; }
    }
}
