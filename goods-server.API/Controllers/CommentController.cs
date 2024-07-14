using artshare_server.WebAPI.ResponseModels;
using goods_server.Contracts;
using goods_server.Service.FilterModel;
using goods_server.Service.InterfaceService;
using Microsoft.AspNetCore.Mvc;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDTO commentDto)
        {
            try
            {
                var result = await _commentService.CreateCommentAsync(commentDto);
                if (!result)
                {
                    return StatusCode(500, new FailedResponseModel
                    {
                        Status = 500,
                        Message = "Create comment failed."
                    });
                }
                return Ok(new SucceededResponseModel()
                {
                    Status = Ok().StatusCode,
                    Message = "Success",
                    Data = new
                    {
                        Comment = commentDto
                    }
                });
            }
            catch (Exception ex)
            {
                return Conflict(new FailedResponseModel()
                {
                    Status = Conflict().StatusCode,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetCommentsByAccountId(Guid accountId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByAccountIdAsync(accountId);
                if (comments == null)
                {
                    return NotFound();
                }
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment(Guid commentId, [FromBody] UpdateCommentDTO commentDto)
        {
            try
            {
                var result = await _commentService.UpdateCommentAsync(commentId, commentDto);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            try
            {
                var result = await _commentService.DeleteCommentAsync(commentId);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetCommentById(Guid commentId)
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsByProductId([FromQuery] CommentFilter filter)
        {
            try
            {
                var comments = await _commentService.GetCommentsByProductIdAsync(filter);
                if (comments == null)
                {
                    return NotFound();
                }
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}
