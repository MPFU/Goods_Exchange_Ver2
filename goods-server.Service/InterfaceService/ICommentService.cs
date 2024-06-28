using goods_server.Contracts;
using goods_server.Service.FilterModel.Helper;
using goods_server.Service.FilterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace goods_server.Service.InterfaceService
{

    public interface ICommentService
    {

        Task<bool> CreateCommentAsync(CommentDTO comment);
        Task<IEnumerable<GetCommentDTO>> GetCommentsByAccountIdAsync(Guid accountId);
        Task<bool> UpdateCommentAsync(Guid commentId, UpdateCommentDTO comment);
        Task<bool> DeleteCommentAsync(Guid commentId);
        Task<GetCommentDTO> GetCommentByIdAsync(Guid commentId);
        Task<IEnumerable<GetCommentDTO>> GetCommentsByProductIdAsync(Guid productId);
        Task<PagedResult<CommentDTO>> GetCommentsByProductIdAsync(CommentFilter filter);


    }


}
