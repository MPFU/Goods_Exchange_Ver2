using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class ReplyCommentService:IReplyCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ReplyCommentService(IUnitOfWork unitOfWork, IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        public async Task<bool> CreateReplyCommentAsync(CreateReplyDTO replyDTO)
        {
            try
            {
                var reply = _mapper.Map<ReplyComment>(replyDTO);
                reply.PostDate = DateTime.UtcNow;
                reply.ReplyId = Guid.NewGuid();
                await _unitOfWork.ReplyCommentRepo.AddAsync(reply);
                var result = await _unitOfWork.SaveAsync() > 0;
                if(result)
                {
                    var comment = await _unitOfWork.CommentRepo.GetByIdAsync(reply.CommentId);
                    if(comment != null)
                    {
                        var check = await _productService.UpdateCommentProduct(comment.ProductId, comment.CommentId);
                        if (check)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }catch(DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteReplyCommentAsync(Guid id)
        {
            try
            {
                var reply = await _unitOfWork.ReplyCommentRepo.GetByIdAsync(id);
                if (reply != null)
                {
                    _unitOfWork.ReplyCommentRepo.Delete(reply);
                   var result = await _unitOfWork.SaveAsync() > 0;
                    return result;
                }
                return false;
            }catch(DbUpdateException)
            {
                throw;
            }
        }

        public async Task<GetReplyCommentDTO?> GetReplyCommentByCommenterIdAsync(Guid id)
        {
            var reply = await _unitOfWork.ReplyCommentRepo.GetReplyCommentByCommenterAsync(id);
            return _mapper.Map<GetReplyCommentDTO?>(reply);
        }

        public async Task<GetReplyCommentDTO?> GetReplyCommentByCommentIdAsync(Guid id)
        {
            var reply = await _unitOfWork.ReplyCommentRepo.GetReplyCommentByCommentIDAsync(id);
            return _mapper.Map<GetReplyCommentDTO?>(reply);
        }

        public async Task<GetReplyCommentDTO?> GetReplyCommentByIdAsync(Guid id)
        {
            var reply = await _unitOfWork.ReplyCommentRepo.GetByIdAsync(id);
            return _mapper.Map<GetReplyCommentDTO?>(reply);
        }

        public async Task<bool> UpdateReplyCommentAsync(Guid id, UpdateReplyCommentDTO replyDTO)
        {
            try
            {
                var reply = await _unitOfWork.ReplyCommentRepo.GetByIdAsync(id);
                if (reply != null)
                {
                    reply.Descript = replyDTO.Descript;
                    _unitOfWork.ReplyCommentRepo.Update(reply);
                    var result = await _unitOfWork.SaveAsync() > 0;
                    return result;
                }
                return false;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

       
    }
}
