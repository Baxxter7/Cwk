using Cwk.Domain.Entities;
using Cwk.Domain.Interfaces;
using Cwk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cwk.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _repository;

    public UserRepository(AppDbContext repository)
    {
        _repository = repository;
    }
    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllAsync() => await _repository.Users
            .AsNoTracking()
            .Where(e => e.IsActive)
            .ToListAsync();

    public Task<User> CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}