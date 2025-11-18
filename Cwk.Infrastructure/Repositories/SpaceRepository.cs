using Cwk.Domain.Entities;
using Cwk.Domain.Interfaces;
using Cwk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cwk.Infrastructure.Repositories;

public class SpaceRepository : ISpaceRepository
{
    private readonly AppDbContext _context;

    public SpaceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Space?> GetSpaceByIdAsync(int id)
        => await _context.Spaces
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<List<Space>> GetAllSpacesAsync()
        => await _context.Spaces.ToListAsync();


    public async Task<Space> AddSpaceAsync(Space space, List<int> amenityIds)
    {
        await _context.Spaces.AddAsync(space);
        await _context.SaveChangesAsync();

        var spaceAmenities = amenityIds.Select(aid => new SpaceAmenity
        {
            SpaceId = space.Id,
            AmenityId = aid
        }).ToList();

        await _context.SpaceAmenities.AddRangeAsync(spaceAmenities);
        await _context.SaveChangesAsync();

        return space;
    }

    public async Task<Space> UpdateSpaceAsync(Space space, List<int> amenityIds)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        
        _context.Spaces.Update(space);
        await _context.SaveChangesAsync();

        var existingAmenities = await _context.SpaceAmenities
            .Where(sa => sa.SpaceId == space.Id)
            .ToListAsync();
        
        _context.SpaceAmenities.RemoveRange(existingAmenities);
        
        if (amenityIds != null && amenityIds.Any())
        {
            var newAmenities = amenityIds
                .Select(aId => new SpaceAmenity
                {
                    SpaceId = space.Id,
                    AmenityId = aId
                }).ToList();

            await _context.SpaceAmenities.AddRangeAsync(newAmenities);
            await _context.SaveChangesAsync();
        }
        
        await transaction.CommitAsync();
        return space;
    }
    
   /*
    -- Codigo original
    public async Task UpdateAsync(Space space, List<int> ints)
    {
        _context.Spaces.Update(space);
        await _context.SaveChangesAsync();

        var spaceAmenities = ints.Select(aid => new SpaceAmenity
        {
            SpaceId = space.Id,
            AmenityId = aid
        }).ToList();

        _context.SpaceAmenities.RemoveRange(
            _context.SpaceAmenities.Where(sa => sa.SpaceId == space.Id)
        );

        await _context.SpaceAmenities.AddRangeAsync(spaceAmenities);
        await _context.SaveChangesAsync();
    }
         */

    public async Task DeleteSpaceAsync(int id)
    {
        var space = await _context.Spaces.FindAsync(id);
        if (space != null)
        {
            _context.Spaces.Remove(space);
            await _context.SaveChangesAsync();
        }
    }
}