using goods_server.Contracts;
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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var check = await _productService.GetProduct(id);
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
                return Ok("Create Success...");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                    return NotFound();
                }
                var pro = await _productService.UpdateProduct(id, updateProduct);
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
                    return NotFound();
                }
                var pro = await _productService.UpdateStatusProduct(id, updateProduct);
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
