using Cwk.Business.Interfaces;
using Cwk.Domain.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cwk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityService _amenityService;
        
        public AmenitiesController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAmenities()
        {
            var amenities = await _amenityService.GetAllAmenitiesAsync();
            return Ok(amenities);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAmenityById(int id)
        {
            var amenity = await _amenityService.GetAmenityByIdAsync(id);
            if (amenity is null)
            {
                return NotFound();
            }
            
            return Ok(amenity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAmenity([FromBody] AddAmenityDto amenityDto)
        {
            var createdAmenity = await _amenityService.CreateAmenityAsync(amenityDto);

            if (createdAmenity is null)
            {
                return BadRequest("No se pudo crear el amenity.");
            }

            return CreatedAtAction(nameof(GetAmenityById), new { id = createdAmenity.Id }, createdAmenity);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAmenity(int id, [FromBody] UpdateAmenityDto amenityDto)
        {
            if (id != amenityDto.Id)
            {
                return BadRequest("Debes ingresar el id");
            }

            await _amenityService.UpdateAmenityAsync(amenityDto);
            return NoContent();
        }

        [HttpGet("spaceId/{spaceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySpaceId(int spaceId) {

            if (spaceId <= 0)
                return BadRequest("El identificador de espacio debe ser mayor a cero.");

            var amenities = await _amenityService.GetAmenitesBySpaceIdAsync(spaceId);

            if (amenities == null || amenities.Count == 0)
                return NotFound($"No se encontraron amenities para el espacio con id {spaceId}.");

            return Ok(amenities);
        }
    }
}
