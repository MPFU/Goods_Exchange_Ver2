using artshare_server.WebAPI.ResponseModels;
using goods_server.Contracts;
using goods_server.Core.Models;
using goods_server.Service.FilterModel;
using goods_server.Service.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAzureBlobStorage _azureBlobStorage;

        public ProductController(IProductService productService, IAzureBlobStorage azureBlobStorage)
        {
            _productService = productService;
            _azureBlobStorage = azureBlobStorage;

        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var check = await _productService.GetProductById(id);
                if (check == null)
                {
                    return NotFound();
                }
                return Ok(check);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet] 
        public async Task<IActionResult> GetAllProduct([FromQuery]ProductFilter productFilter)
        {
            try
            {
                var pro = await _productService.GetAllProductAsync<Product>(productFilter);
                return Ok(pro);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProduct)
        {
            try
            {
               var result = await _productService.CreateProduct(createProduct);
                if (!result)
                {
                    return BadRequest("Create Fail!...");
                }
                return Ok(new SucceededResponseModel(){
                    Status = Ok().StatusCode,
                    Message = "Create Product Success...", 
                });
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileProductImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(new FailedResponseModel()
                    {
                        Status = BadRequest().StatusCode,
                        Message = "File is not selected or empty."
                    });
                var imageExtensions = new[] { ".jpg", ".jpeg", ".png" };
                if (imageExtensions.Any(e => file.FileName.EndsWith(e, StringComparison.OrdinalIgnoreCase)) == false)
                {
                    return BadRequest(new FailedResponseModel()
                    {
                        Status = BadRequest().StatusCode,
                        Message = "File is not image."
                    });
                }
                var containerName = "product"; // replace with your container name
                var uri = await _azureBlobStorage.UploadFileAsync(containerName, file);

                return Ok(new SucceededResponseModel()
                {
                    Status = Ok().StatusCode,
                    Message = "File uploaded successfully",
                    Data = new
                    {
                        FileUri = uri
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new FailedResponseModel
                {
                    Status = 500,
                    Message = $"An error occurred while uploading the file: {ex.Message}"
                });
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDTO updateProduct)
        {
            try
            {
                var check = await _productService.GetProduct(id);
                if (check == null)
                {
                    return BadRequest(new FailedResponseModel
                    {
                        Status = BadRequest().StatusCode,
                        Message = "Not Found This Product!"
                    });
                }
                var pro = await _productService.UpdateProduct(id, updateProduct);
                if (pro)
                {
                    return Ok(new SucceededResponseModel
                    {
                        Status= Ok().StatusCode,
                        Message = "Update SUCCESS..."
                    });
                }
                return BadRequest(new FailedResponseModel
                {
                    Status = BadRequest().StatusCode,
                    Message = "Update Fail"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new FailedResponseModel
                {
                    Status = BadRequest().StatusCode,
                    Message = ex.ToString()
                });
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateRatingCommentProduct(Guid id, [FromBody] UpdateRatingProductDTO updateProduct)
        {
            try
            {
                var check = await _productService.GetProduct(id);
                if (check == null)
                {
                    return NotFound();
                }
                var pro = await _productService.UpdateRatingCommentProduct(id, updateProduct);
                if (pro)
                {
                    return Ok();
                }
                return BadRequest("Update Fail");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("id")]
        public async Task<IActionResult> UpdateStatusProduct(Guid id, [FromBody] UpdateStatusProductDTO updateProduct)
        {
            try
            {
                var check = await _productService.GetProduct(id);
                if (check == null)
                {
                    return BadRequest(new FailedResponseModel
                    {
                        Status = BadRequest().StatusCode,
                        Message = "Not Found That Product"
                    });
                }
                var pro = await _productService.UpdateStatusProduct(id, updateProduct);
                if (pro)
                {
                    return Ok(new SucceededResponseModel
                    {
                        Status = Ok().StatusCode,
                        Message = "Update Status Success...",
                    });
                }
                return BadRequest("Update Fail!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var check = await _productService.GetProduct(id);
                if (check == null)
                {
                    return NotFound();
                }
                var pro = await _productService.DeleteProduct(id);
                if (pro)
                {
                    return Ok();
                }
                return BadRequest("Update Fail");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
