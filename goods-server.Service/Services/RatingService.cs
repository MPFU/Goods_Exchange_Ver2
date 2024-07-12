using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.FilterModel;
using goods_server.Service.FilterModel.Helper;
using goods_server.Service.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<PagedResult<RatingDTO>> GetAllRatingsAsync(RatingFilter filter)
        {
            var ratings = await _unitOfWork.RatingRepo.GetAllAsync();
            var query = ratings.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query = filter.SortAscending ? query.OrderBy(r => GetProperty.GetPropertyValue(r, filter.SortBy)) : query.OrderByDescending(r => GetProperty.GetPropertyValue(r, filter.SortBy));
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / filter.PageSize);
            var items = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            var result = new PagedResult<RatingDTO>
            {
                Items = _mapper.Map<IEnumerable<RatingDTO>>(items),
                TotalItem = totalItems,
                PageSize = filter.PageSize,
                TotalPages = totalPages,
                PageNumber = filter.PageNumber
            };

            return result;
        }

        public async Task<RatingDTO?> GetRatingByCustomerAndProductIdAsync(Guid customerId, Guid productId)
        {
            var rating = await _unitOfWork.RatingRepo.GetByCustomerAndProductIdAsync(customerId, productId);
            return _mapper.Map<RatingDTO>(rating);
        }

        public async Task<PagedResult<RatingDTO>> GetRatingsByProductIdAsync(RatingFilter filter)
        {
            var ratings = await _unitOfWork.RatingRepo.FindAsync(r => r.ProductId == filter.ProductId);
            var query = ratings.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query = filter.SortAscending ? query.OrderBy(r => GetProperty.GetPropertyValue(r, filter.SortBy)) : query.OrderByDescending(r => GetProperty.GetPropertyValue(r, filter.SortBy));
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / filter.PageSize);
            var items = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            var result = new PagedResult<RatingDTO>
            {
                Items = _mapper.Map<IEnumerable<RatingDTO>>(items),
                TotalItem = totalItems,
                PageSize = filter.PageSize,
                TotalPages = totalPages,
                PageNumber = filter.PageNumber
            };

            return result;
        }

        public async Task<bool> CreateRatingAsync(CreateRatingDTO rating)
        {
            var newRating = _mapper.Map<Rating>(rating);
            newRating.Id = Guid.NewGuid();
            newRating.CreatedDate = DateTime.UtcNow;
            await _unitOfWork.RatingRepo.AddAsync(newRating);
            var check = await _unitOfWork.SaveAsync() > 0;
            if (check)
            {
                var result = await _productService.UpdateRatingProduct(rating.ProductId, rating.Rated);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateRatingAsync(Guid ratingId, UpdateRatingDTO rating)
        {
            var existingRating = await _unitOfWork.RatingRepo.GetByIdAsync(ratingId);
            if (existingRating == null)
            {
                return false;
            }

            _mapper.Map(rating, existingRating);
            _unitOfWork.RatingRepo.Update(existingRating);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteRatingAsync(Guid ratingId)
        {
            var rating = await _unitOfWork.RatingRepo.GetByIdAsync(ratingId);
            if (rating == null)
            {
                return false;
            }

            _unitOfWork.RatingRepo.Delete(rating);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
