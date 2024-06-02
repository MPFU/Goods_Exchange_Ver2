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

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RatingDTO>> GetAllRatingsAsync()
        {
            var ratings = await _unitOfWork.RatingRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<RatingDTO>>(ratings);
        }

        public async Task<RatingDTO?> GetRatingByProductIdAsync(Guid productId)
        {
            var rating = await _unitOfWork.RatingRepo.GetByProductIdAsync(productId);
            return _mapper.Map<RatingDTO>(rating);
        }

        public async Task<bool> CreateRatingAsync(CreateRatingDTO rating)
        {
            var newRating = _mapper.Map<Rating>(rating);
            newRating.ProductId = rating.ProductId;
            await _unitOfWork.RatingRepo.AddAsync(newRating);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateRatingAsync(Guid productId, UpdateRatingDTO rating)
        {
            var existingRating = await _unitOfWork.RatingRepo.GetByProductIdAsync(productId);
            if (existingRating == null)
            {
                return false;
            }

            _mapper.Map(rating, existingRating);
            _unitOfWork.RatingRepo.Update(existingRating);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteRatingAsync(Guid productId)
        {
            var rating = await _unitOfWork.RatingRepo.GetByProductIdAsync(productId);
            if (rating == null)
            {
                return false;
            }

            _unitOfWork.RatingRepo.Delete(rating);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
