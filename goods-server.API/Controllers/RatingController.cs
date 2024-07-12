using goods_server.Contracts;
using goods_server.Service.FilterModel;
using goods_server.Service.InterfaceService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRatings([FromQuery] RatingFilter filter)
        {
            var ratings = await _ratingService.GetAllRatingsAsync(filter);
            return Ok(ratings);
        }

        [HttpGet("{customerId}/{productId}")]
        public async Task<IActionResult> GetRatingByCustomerAndProductId(Guid customerId, Guid productId)
        {
            var rating = await _ratingService.GetRatingByCustomerAndProductIdAsync(customerId, productId);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }

        [HttpGet("ByProduct")]
        public async Task<IActionResult> GetRatingsByProductId([FromQuery] RatingFilter filter)
        {
            var ratings = await _ratingService.GetRatingsByProductIdAsync(filter);
            return Ok(ratings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] CreateRatingDTO createRatingDTO)
        {
            var result = await _ratingService.CreateRatingAsync(createRatingDTO);
            if (result)
            {
                return Ok("Rating created successfully.");
            }
            return BadRequest("Failed to create rating.");
        }

        [HttpPut("{ratingId}")]
        public async Task<IActionResult> UpdateRating(Guid ratingId, [FromBody] UpdateRatingDTO updateRatingDTO)
        {
            var result = await _ratingService.UpdateRatingAsync(ratingId, updateRatingDTO);
            if (result)
            {
                return Ok("Rating updated successfully.");
            }
            return BadRequest("Failed to update rating.");
        }

        [HttpDelete("{ratingId}")]
        public async Task<IActionResult> DeleteRating(Guid ratingId)
        {
            var result = await _ratingService.DeleteRatingAsync(ratingId);
            if (result)
            {
                return Ok("Rating deleted successfully.");
            }
            return BadRequest("Failed to delete rating.");
        }
    }
}
