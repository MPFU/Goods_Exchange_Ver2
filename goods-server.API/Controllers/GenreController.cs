using goods_server.Contracts;
using goods_server.Service.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(Guid id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDTO createGenreDTO)
        {
            var result = await _genreService.CreateGenreAsync(createGenreDTO);
            if (result)
            {
                return Ok("Genre created successfully.");
            }
            return BadRequest("Failed to create genre.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(Guid id, [FromBody] UpdateGenreDTO updateGenreDTO)
        {
            var result = await _genreService.UpdateGenreAsync(id, updateGenreDTO);
            if (result)
            {
                return Ok("Genre updated successfully.");
            }
            return BadRequest("Failed to update genre.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            var (success, message) = await _genreService.DeleteGenreAsync(id);
            if (success)
            {
                return Ok(message);
            }
            return BadRequest(message);
        }
    }
}
