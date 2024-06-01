using goods_server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IReplyCommentService
    {
        Task<bool> CreateReplyCommentAsync(CreateReplyDTO replyDTO);
        Task<bool> UpdateReplyCommentAsync(Guid id, UpdateReplyCommentDTO replyDTO);
        Task<bool> DeleteReplyCommentAsync(Guid id);
        Task<GetReplyCommentDTO?> GetReplyCommentByCommentIdAsync(Guid id);
        Task<GetReplyCommentDTO?> GetReplyCommentByCommenterIdAsync(Guid id);
        Task<GetReplyCommentDTO?> GetReplyCommentByIdAsync(Guid id);
    }
}
