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
    public async Task<User?> GetByIdAsync(int id)  =>  await _repository.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Id == id && u.IsActive );

    public async Task<IEnumerable<User>> GetAllAsync() => await _repository.Users
            .AsNoTracking()
            .Where(u => u.IsActive)
            .ToListAsync();

    public async Task<User> CreateAsync(User user)
    {
        await _repository.Users.AddAsync(user);
        await _repository.SaveChangesAsync();
        return  user;
    }

    public async Task<User> UpdateAsync(User user)
    {
         _repository.Update(user);
        await _repository.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email) =>  await _repository.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower()  && u.IsActive );
}