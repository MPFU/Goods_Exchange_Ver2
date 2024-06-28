using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<RatingDTO>> GetAllRatingsAsync()
        {
            var ratings = await _unitOfWork.RatingRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<RatingDTO>>(ratings);
        }

        public async Task<RatingDTO?> GetRatingByCustomerAndProductIdAsync(Guid customerId, Guid productId)
        {
            var rating = await _unitOfWork.RatingRepo.GetByCustomerAndProductIdAsync(customerId, productId);
            return _mapper.Map<RatingDTO>(rating);
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
