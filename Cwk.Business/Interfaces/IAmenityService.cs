using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;
using Cwk.Domain.Entities;

namespace Cwk.Business.Interfaces;

public interface IAmenityService
{
    Task<List<AmenityResponseDto>> GetAllAmenitiesAsync();
    Task<AmenityResponseDto> GetAmenityByIdAsync(int amenityId);
    Task UpdateAmenityAsync(UpdateAmenityDto amenityDto);
    Task<AmenityResponseDto> CreateAmenityAsync(AddAmenityDto amenityDto);
}