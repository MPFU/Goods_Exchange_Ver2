using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;

namespace goods_server.Service.FilterModel
{
    public class RequestHistoryFilter : FilterOption<RequestHistory>
    {
        public Guid AccountId { get; set; }
        public Guid? BuyerId { get; set; }
        public Guid? SellerId { get; set; }
        public Guid? ProductSellerId { get; set; }
        public Guid? ProductBuyerId { get; set; }
        public string Status { get; set; }
    }
}
