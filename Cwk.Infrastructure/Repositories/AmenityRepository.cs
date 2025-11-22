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

    public async Task<Amenity?> GetByAmenityId(int amenityId) => await _context.Amenities.FindAsync(amenityId);


    public async Task<Amenity> AddAmenityAsync(Amenity amenity)
    {
        await _context.AddAsync(amenity);
        await _context.SaveChangesAsync();
        return amenity;
    }

    public async Task<List<Amenity>> GetAllAmenitiesAsync() => await _context.Amenities.ToListAsync();

    public async Task UpdateAmenityAsync(Amenity amenity)
    {
        _context.Amenities.Update(amenity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Amenity>> GetAmenitesBySpaceIdAsync(int spaceId)
    {
        return await _context.SpaceAmenities
            .Where(sa => sa.SpaceId == spaceId)
            .Select(sa => sa.Amenity)
            .ToListAsync();
    }
}