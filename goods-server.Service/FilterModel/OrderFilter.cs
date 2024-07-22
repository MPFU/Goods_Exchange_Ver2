using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;

public class OrderFilter : FilterOption<Order>
{
    public Guid? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? Status { get; set; }
}
