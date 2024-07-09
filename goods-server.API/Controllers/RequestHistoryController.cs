using artshare_server.WebAPI.ResponseModels;
using goods_server.Contracts;
using goods_server.Service.FilterModel;
using goods_server.Service.InterfaceService;
using goods_server.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RequestHistoryController : ControllerBase
    {
        private readonly IRequestHistoyService _requestHistoryService;

        public RequestHistoryController(IRequestHistoyService requestHistoryService)
        {
            _requestHistoryService = requestHistoryService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateRequestHistory([FromBody] RequestHistoryDTO requestHistoryDto)
        {
            try
            {
                var result = await _requestHistoryService.CreateRequestHistoryAsync(requestHistoryDto);
                if (!result)
                {
                    return StatusCode(500, new FailedResponseModel
                    {
                        Status = 500,
                        Message = "Create request history failed."
                    });
                }
                return Ok(new SucceededResponseModel()
                {
                    Status = Ok().StatusCode,
                    Message = "Success",
                    Data = new
                    {
                        RequestHistory = requestHistoryDto
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

        [HttpGet]
        public async Task<IActionResult> GetRequestHistoriesByAccountId([FromQuery] RequestHistoryFilter filter)
        {
            var pagedResult = await _requestHistoryService.GetRequestHistoriesByAccountIdAsync(filter);
            return Ok(pagedResult);
        }

        [HttpGet("{requestHistoryId}")]
        public async Task<IActionResult> GetRequestHistoryById(Guid requestHistoryId)
        {
            var requestHistory = await _requestHistoryService.GetRequestHistoryByIdAsync(requestHistoryId);
            if (requestHistory == null)
            {
                return NotFound();
            }
            return Ok(requestHistory);
        }


        [HttpPut("{requestId}")]
        public async Task<IActionResult> UpdateRequestHistory(Guid requestId, [FromBody] UpdateRequestHistoryDTO requestHistoryDto)
        {
            try
            {
                var result = await _requestHistoryService.UpdateRequestHistoryAsync(requestId, requestHistoryDto);
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

        [HttpDelete("{requestId}")]
        public async Task<IActionResult> DeleteRequestHistory(Guid requestId)
        {
            try
            {
                var result = await _requestHistoryService.DeleteRequestHistoryAsync(requestId);
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

        [HttpGet]
        public async Task<IActionResult> GetAllRequestHistories()
        {
            var requestHistories = await _requestHistoryService.GetAllRequestHistoriesAsync();
            return Ok(requestHistories);
        }
    }
}
