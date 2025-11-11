using AutoMapper;
using Cwk.Application.Interfaces;
using Cwk.Business.Interfaces;
using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;
using Cwk.Domain.Entities;
using Cwk.Domain.Interfaces;

namespace Cwk.Business.Services;

public class AuthService :IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthService(ITokenService tokenService, IUserRepository userRepository, IMapper mapper)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequestDto.Email);
        var isAuthenticated = user != null && 
                              BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.PasswordHash);
        if (isAuthenticated && user!.IsActive)
        {
            var token = _tokenService.GenerateToken(user!);
            return new LoginResponseDto { IsAuthenticated = true, Token = token, Message = "Inicio de sesión exitoso"};
        }
        else
        {
            return new LoginResponseDto { IsAuthenticated = false, Token = string.Empty, Message = "Credenciales incorrectas" };
        }
    }

    public async Task<UserResponseDto> Register(CreateUserDto createUserDto)
    {
        var user = new User()
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
            Role = createUserDto.Role
        };

        var createdUser = await _userRepository.CreateAsync(user);
        return _mapper.Map<UserResponseDto>(createdUser);
    }
}