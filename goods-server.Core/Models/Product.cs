using System;
using System.Collections.Generic;

namespace goods_server.Core.Models;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ImagePro { get; set; }

    public Guid? CreatorId { get; set; }

    public decimal? Price { get; set; }

    public Guid? CategoryId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Rated { get; set; }

    public int? RatedCount { get; set; }

    public int? CommentCount { get; set; }

    public int? Discount { get; set; }

    public int? Quantity { get; set; }

    public Guid? CityId { get; set; }

    public string? DenyRes { get; set; }

    public string? Status { get; set; }

    public Guid? GenreId { get; set; }

    public string? IsDisplay { get; set; }

    public virtual Category? Category { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<RequestHistory> RequestHistoryProductBuyers { get; set; } = new List<RequestHistory>();

    public virtual ICollection<RequestHistory> RequestHistoryProductSellers { get; set; } = new List<RequestHistory>();
}
