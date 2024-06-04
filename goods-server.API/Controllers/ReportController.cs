using artshare_server.WebAPI.ResponseModels;
using goods_server.Contracts;
using goods_server.Service.InterfaceService;
using goods_server.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportDTO reportDto)
        {
            try
            {
                var result = await _reportService.CreateReportAsync(reportDto);
                if (result)
                {
                    return Ok(new SucceededResponseModel()
                    {
                        Status = Ok().StatusCode,
                        Message = "Success",
                        Data = new
                        {
                            Report = reportDto
                        }
                    });
                }
                return StatusCode(500, new FailedResponseModel
                {
                    Status = 500,
                    Message = "Create report failed."
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
        public async Task<IActionResult> GetReportsByAccountId(Guid accountId)
        {
            try
            {
                var reports = await _reportService.GetReportsByAccountIdAsync(accountId);
                if (reports == null)
                {
                    return NotFound();
                }
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetReportById(Guid reportId)
        {
            var report = await _reportService.GetReportByIdAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }


        [HttpPut("{reportId}")]
        public async Task<IActionResult> UpdateReport(Guid reportId, [FromBody] UpdateReportDTO reportDto)
        {
            try
            {
                var result = await _reportService.UpdateReportAsync(reportId, reportDto);
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

        [HttpDelete("{reportId}")]
        public async Task<IActionResult> DeleteReport(Guid reportId)
        {
            try
            {
                var result = await _reportService.DeleteReportAsync(reportId);
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
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }
    }

}
