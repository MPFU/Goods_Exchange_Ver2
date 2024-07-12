using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;

namespace goods_server.Service.FilterModel
{
    public class RequestHistoryFilter : FilterOption<RequestHistory>
    {
        public Guid AccountId { get; set; }
    }
}
