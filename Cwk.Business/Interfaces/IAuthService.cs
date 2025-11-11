using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;

namespace Cwk.Business.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<UserResponseDto> Register(CreateUserDto createUserDto);
}