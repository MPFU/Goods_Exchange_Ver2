using System;
using System.Collections.Generic;

namespace goods_server.Core.Models;

public partial class Account
{
    public Guid AccountId { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? AvatarUrl { get; set; }

    public string? UserName { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? JoinDate { get; set; }

    public int? RoleId { get; set; }

    public string? DenyRes { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<ReplyComment> ReplyComments { get; set; } = new List<ReplyComment>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<RequestHistory> RequestHistoryBuyers { get; set; } = new List<RequestHistory>();

    public virtual ICollection<RequestHistory> RequestHistorySellers { get; set; } = new List<RequestHistory>();

    public virtual Role? Role { get; set; }
}
