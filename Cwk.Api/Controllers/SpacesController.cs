using Cwk.Business.Interfaces;
using Cwk.Domain.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cwk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacesController : ControllerBase
    {
        private readonly ISpaceService _spaceService;

        public SpacesController(ISpaceService spaceRepository)
        {
            _spaceService = spaceRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSpaces() =>
            Ok(await _spaceService.GetAllSpacesAsync());

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSpaceById(int id)
        {
            var space = await _spaceService.GetSpaceByIdAsync(id);
            return space is null ? NotFound() : Ok(space);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] AddSpaceDto spaceDto)
        {
            var createdSpace = await _spaceService.CreateSpaceAsync(spaceDto);
            return CreatedAtAction(nameof(GetSpaceById), new { id = createdSpace.Id }, createdSpace);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody] EditSpaceDto spaceDto)
        {
            if(id != spaceDto.Id) return BadRequest();
            
            await _spaceService.UpdateSpaceAsync(spaceDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _spaceService.DeleteSpaceAsync(id);
            return NoContent();
        }
        
    }
}
