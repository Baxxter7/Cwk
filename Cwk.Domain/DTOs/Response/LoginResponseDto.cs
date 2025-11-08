namespace Cwk.Domain.DTOs.Response;

public class LoginResponseDto
{
    public bool IsAuthenticated { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}