using AutoMapper;
using Cwk.Business.Interfaces;
using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;
using Cwk.Domain.Entities;
using Cwk.Domain.Interfaces;
namespace Cwk.Business.Services;

public class AmenityService : IAmenityService
{
    private readonly IAmenityRepository _amenityRepository;
    private readonly IMapper _mapper;

    public AmenityService(IAmenityRepository amenityRepository, IMapper mapper)
    {
        _amenityRepository = amenityRepository;
        _mapper = mapper;
    }

    public async Task<List<AmenityResponseDto>> GetAllAmenitiesAsync()
    {
        var amenities = await _amenityRepository.GetAllAmenitiesAsync();
        return _mapper.Map<List<AmenityResponseDto>>(amenities);
    }

    public async Task<AmenityResponseDto> GetAmenityByIdAsync(int amenityId)
    {
        var amenity = await _amenityRepository.GetByAmenityId(amenityId);
        return _mapper.Map<AmenityResponseDto>(amenity);
    }

    public async Task UpdateAmenityAsync(UpdateAmenityDto amenityDto)
    {
        var amenity = _mapper.Map<Amenity>(amenityDto);
        await _amenityRepository.UpdateAmenityAsync(amenity);
    }

    public async Task<AmenityResponseDto> CreateAmenityAsync(AddAmenityDto amenityDto)
    {
        var amenity = _mapper.Map<Amenity>(amenityDto);
        var createdAmenity = await _amenityRepository.AddAmenityAsync(amenity);
        return _mapper.Map<AmenityResponseDto>(createdAmenity);
    }

    public async Task<List<AmenityResponseDto>> GetAmenitesBySpaceIdAsync(int spaceId)
    {
        var amenities = await _amenityRepository.GetAmenitesBySpaceIdAsync(spaceId);
        return _mapper.Map<List<AmenityResponseDto>>(amenities);
    }
}