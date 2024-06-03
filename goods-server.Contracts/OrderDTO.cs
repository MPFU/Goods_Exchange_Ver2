using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Contracts
{
    public class OrderDTO
    {
        public Guid? CustomerId { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; }
    }

    public class UpdateOrderDTO : OrderDTO
    {
        public Guid OrderId { get; set; }
    }

}
