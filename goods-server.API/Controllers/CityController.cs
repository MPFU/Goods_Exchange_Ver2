using goods_server.Contracts;
using goods_server.Service.InterfaceService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(Guid id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityDTO createCityDTO)
        {
            var result = await _cityService.CreateCityAsync(createCityDTO);
            if (result)
            {
                return Ok("City created successfully.");
            }
            return BadRequest("Failed to create city.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(Guid id, [FromBody] UpdateCityDTO updateCityDTO)
        {
            var result = await _cityService.UpdateCityAsync(id, updateCityDTO);
            if (result)
            {
                return Ok("City updated successfully.");
            }
            return BadRequest("Failed to update city.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var (success, message) = await _cityService.DeleteCityAsync(id);
            if (success)
            {
                return Ok(message);
            }
            return BadRequest(message);
        }
    }
}
