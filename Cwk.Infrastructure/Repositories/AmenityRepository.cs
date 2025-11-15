using Cwk.Domain.Entities;
using Cwk.Domain.Interfaces;
using Cwk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cwk.Infrastructure.Repositories;

public class AmenityRepository : IAmenityRepository
{
    private readonly AppDbContext _context;

    public AmenityRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Amenity> GetByAmenityId(int amenityId)
    {
        throw new NotImplementedException();
    }

    public Task<Amenity> AddAmenityAsync(Amenity amenity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Amenity>> GetAllAmenitiesAsync() => await _context.Amenities.ToListAsync();
    
    public Task UpdateAmenityAsync(Amenity amenity)
    {
        throw new NotImplementedException();
    }
}