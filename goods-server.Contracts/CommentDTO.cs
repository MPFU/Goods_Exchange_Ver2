using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Contracts
{
    public class CommentDTO
    {
        public Guid? CommenterId { get; set; }
        public Guid? ProductId { get; set; }
        public string? Descript { get; set; }
    }

    public class UpdateCommentDTO : CommentDTO
    {
        public Guid CommentId { get; set; }
    }

}
