using goods_server.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingDTO>> GetAllRatingsAsync();
        Task<RatingDTO?> GetRatingByProductIdAsync(Guid productId);
        Task<bool> CreateRatingAsync(CreateRatingDTO rating);
        Task<bool> UpdateRatingAsync(Guid productId, UpdateRatingDTO rating);
        Task<bool> DeleteRatingAsync(Guid productId);
    }
}
