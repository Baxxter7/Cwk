using Cwk.Domain.Entities;

namespace Cwk.Domain.Interfaces;

public interface IAmenityRepository
{
    Task<Amenity?>  GetByAmenityId(int amenityId);
    Task<Amenity> AddAmenityAsync(Amenity amenity);
    Task<List<Amenity>> GetAllAmenitiesAsync();
    Task UpdateAmenityAsync(Amenity amenity);
    Task<List<Amenity>> GetAmenitesBySpaceIdAsync(int spaceId);
}