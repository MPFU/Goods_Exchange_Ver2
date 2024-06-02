using goods_server.Contracts;
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
        public async Task<IActionResult> GetAllRatings()
        {
            var ratings = await _ratingService.GetAllRatingsAsync();
            return Ok(ratings);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetRatingByProductId(Guid productId)
        {
            var rating = await _ratingService.GetRatingByProductIdAsync(productId);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
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

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateRating(Guid productId, [FromBody] UpdateRatingDTO updateRatingDTO)
        {
            var result = await _ratingService.UpdateRatingAsync(productId, updateRatingDTO);
            if (result)
            {
                return Ok("Rating updated successfully.");
            }
            return BadRequest("Failed to update rating.");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteRating(Guid productId)
        {
            var result = await _ratingService.DeleteRatingAsync(productId);
            if (result)
            {
                return Ok("Rating deleted successfully.");
            }
            return BadRequest("Failed to delete rating.");
        }
    }
}
