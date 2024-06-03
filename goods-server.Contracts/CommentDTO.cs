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

    public class GetCommentDTO
    {
        public Guid CommentId { get; set; }

        public Guid? CommenterId { get; set; }

        public Guid? ProductId { get; set; }

        public DateTime? PostDate { get; set; }

        public string? Descript { get; set; }

    }


    public class UpdateCommentDTO
    {
      public string? Descript { get; set; }

    }

}
