using System;
using System.Collections.Generic;

namespace goods_server.Core.Models;

public partial class Comment
{
    public Guid CommentId { get; set; }

    public Guid? CommenterId { get; set; }

    public Guid? ProductId { get; set; }

    public DateTime? PostDate { get; set; }

    public string? Descript { get; set; }

    public virtual Account? Commenter { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ICollection<ReplyComment> ReplyComments { get; set; } = new List<ReplyComment>();
}
