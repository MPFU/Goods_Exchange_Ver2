using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Contracts
{
    public class RequestHistoryDTO
    {
        public Guid? BuyerId { get; set; }
        public Guid? SellerId { get; set; }
        public Guid? ProductSellerId { get; set; }
        public Guid? ProductBuyerId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class GetRequestHistoryDTO : RequestHistoryDTO
    {
        public Guid Id { get; set; }
    }

    public class GetRequestHistory2DTO 
    {
        public Guid Id { get; set; }

        public Guid? BuyerId { get; set; }

        public Guid? SellerId { get; set; }

        public Guid? ProductSellerId { get; set; }

        public Guid? ProductBuyerId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? Status { get; set; }


    }



    public class UpdateRequestHistoryDTO
    {
        public Guid? ProductSellerId { get; set; }

        public Guid? ProductBuyerId { get; set; }
        public string? Status { get; set; }
        public Guid? BuyerId { get; set; }
        public Guid? SellerId { get; set; }
    }

    public class UpdateRequestHistoryStatusDTO
    {
        public string Status { get; set; }
    }

}
