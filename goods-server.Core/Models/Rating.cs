using System;
using System.Collections.Generic;

namespace goods_server.Core.Models;

public partial class Rating
{
    public Guid Id { get; set; }

    public Guid? CustomerId { get; set; }

    public Guid? ProductId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Descript { get; set; }

    public int? Rated { get; set; }

    public virtual Account? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
