using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;
using goods_server.Service.FilterModel;
using goods_server.Service.InterfaceService;
using Microsoft.Identity.Client;
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
        private readonly IProductService _productService;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        public async Task<bool> CreateCommentAsync(CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            comment.CommentId = Guid.NewGuid(); // Tạo một Guid mới
            comment.PostDate = DateTime.UtcNow;
            await _unitOfWork.CommentRepo.AddAsync(comment);
            var result = await _unitOfWork.SaveAsync() > 0;
            if (result)
            {
                var check = await _productService.UpdateCommentProduct(commentDto.ProductId,comment.CommentId);
                if (check)
                {
                    return true;
                }
            }
            return false;
        }


        public async Task<IEnumerable<GetCommentDTO>> GetCommentsByAccountIdAsync(Guid accountId)
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentsByAccountIdAsync(accountId);
            return _mapper.Map<IEnumerable<GetCommentDTO>>(comments);
        }

        public async Task<bool> UpdateCommentAsync(Guid commentId, UpdateCommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            return await _unitOfWork.CommentRepo.UpdateCommentAsync(commentId, comment);
        }

     
        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentByIdAsync(commentId);
            if (comments != null)
            {
                //var check = await _productService.UpdateCommentProduct(comments.ProductId, false);               
                _unitOfWork.CommentRepo.Delete(comments);
                var result = await _unitOfWork.SaveAsync() > 0;
                if (result)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<GetCommentDTO> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await _unitOfWork.CommentRepo.GetCommentByIdAsync(commentId);
            return _mapper.Map<GetCommentDTO>(comment);
        }

        public async Task<IEnumerable<GetCommentDTO>> GetCommentsByProductIdAsync(Guid productId)
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentsByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<GetCommentDTO>>(comments);
        }

        public async Task<PagedResult<GetCommentDTO>> GetCommentsByProductIdAsync(CommentFilter filter)
        {
            var commentList = await _unitOfWork.CommentRepo.GetCommentsByProductIdAsync(filter.ProductId);
            var getCommentDTOs = _mapper.Map<IEnumerable<GetCommentDTO>>(commentList);
            IQueryable<GetCommentDTO> filterComment = getCommentDTOs.AsQueryable();

            // Paging
            var pageItems = filterComment
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return new PagedResult<GetCommentDTO>
            {
                Items = pageItems,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalItem = getCommentDTOs.Count(),
                TotalPages = (int)Math.Ceiling((decimal)getCommentDTOs.Count() / filter.PageSize)
            };
        }




    }


}
