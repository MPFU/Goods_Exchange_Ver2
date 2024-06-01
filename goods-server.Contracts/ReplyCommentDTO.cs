using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Contracts
{
    public class ReplyCommentDTO
    {
        public Guid? CommentId { get; set; }

        public Guid? CommenterId { get; set; }

        public string? Descript { get; set; }

    }

    public class CreateReplyDTO : ReplyCommentDTO
    {

    }

    public class UpdateReplyCommentDTO
    {
        public string? Descript { get; set; }
    }

    public class GetReplyCommentDTO : ReplyCommentDTO
    {
        public Guid ReplyId { get; set; }
    }
}
