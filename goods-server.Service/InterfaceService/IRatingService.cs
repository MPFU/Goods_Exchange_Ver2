using goods_server.Contracts;
using goods_server.Service.FilterModel;
using goods_server.Service.FilterModel.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IRatingService
    {
        Task<PagedResult<RatingDTO>> GetAllRatingsAsync(RatingFilter filter);
        Task<RatingDTO?> GetRatingByCustomerAndProductIdAsync(Guid customerId, Guid productId);
        Task<PagedResult<RatingDTO>> GetRatingsByProductIdAsync(RatingFilter filter);
        Task<bool> CreateRatingAsync(CreateRatingDTO rating);
        Task<bool> UpdateRatingAsync(Guid ratingId, UpdateRatingDTO rating);
        Task<bool> DeleteRatingAsync(Guid ratingId);
    }
}
