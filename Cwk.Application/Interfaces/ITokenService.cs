using Cwk.Domain.Entities;

namespace Cwk.Application.Interfaces;

public interface ITokenService
{
    public string GenerateToken(User user);
}