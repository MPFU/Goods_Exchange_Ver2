using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateCommentAsync(CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            comment.CommentId = Guid.NewGuid(); // Tạo một Guid mới
            comment.PostDate = DateTime.UtcNow;
            await _unitOfWork.CommentRepo.AddAsync(comment);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result;
        }


        public async Task<IEnumerable<CommentDTO>> GetCommentsByAccountIdAsync(Guid accountId)
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentsByAccountIdAsync(accountId);
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public async Task<bool> UpdateCommentAsync(Guid commentId, CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            return await _unitOfWork.CommentRepo.UpdateCommentAsync(commentId, comment);
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            return await _unitOfWork.CommentRepo.DeleteCommentAsync(commentId);
        }


    }


}
