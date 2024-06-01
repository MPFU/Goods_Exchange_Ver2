using System;
using System.Collections.Generic;

namespace goods_server.Core.Models;

public partial class Report
{
    public Guid ReportId { get; set; }

    public Guid? AccountId { get; set; }

    public Guid? ProductId { get; set; }

    public DateTime? PostDate { get; set; }

    public string? Descript { get; set; }

    public string? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Product? Product { get; set; }
}
