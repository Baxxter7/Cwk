using Cwk.Business.Interfaces;
using Cwk.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cwk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto? loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid login request");
            }

            var response = await _authService.Login(loginRequest);
            
            return response.IsAuthenticated 
                ? Ok(response) 
                : Unauthorized(response);
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto? createUserDto)
        {
            if (createUserDto == null)
            {
                return BadRequest("Invalid registration request");
            }

            try
            {
                var response = await _authService.Register(createUserDto);
            
                return response != null ?
                    CreatedAtAction(nameof(Register), new { id = response.Id }, response)
                    : BadRequest("Registration failed");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return Conflict(new { Message = "Ese correo ya esta siendo utilizado por otro usuario", isSuccess = false });
                }

                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException!.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
        
    }

}
