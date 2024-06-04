using System;
using System.Collections.Generic;

namespace goods_server.Core.Models;

public partial class RequestHistory
{
    public Guid Id { get; set; }

    public Guid? BuyerId { get; set; }

    public Guid? SellerId { get; set; }

    public Guid? ProductSellerId { get; set; }

    public Guid? ProductBuyerId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Status { get; set; }

    public virtual Account? Buyer { get; set; }

    public virtual Product? ProductBuyer { get; set; }

    public virtual Product? ProductSeller { get; set; }

    public virtual Account? Seller { get; set; }
    
}
