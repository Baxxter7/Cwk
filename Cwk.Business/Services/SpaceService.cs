using AutoMapper;
using Cwk.Application.Interfaces;
using Cwk.Business.Interfaces;
using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;
using Cwk.Domain.Entities;
using Cwk.Domain.Interfaces;

namespace Cwk.Business.Services;

public class SpaceService : ISpaceService
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly IMapper _mapper;
    private readonly IPhotoService  _photoService;

    public SpaceService(ISpaceRepository spaceRepository, IMapper mapper, IPhotoService photoService)
    {
        _spaceRepository = spaceRepository;
        _mapper = mapper;
        _photoService = photoService;
    }

    public async Task<SpaceResponseDto> GetAllSpacesAsync()
    {
        var spaces = await _spaceRepository.GetAllSpacesAsync();
        var spacesDtos = _mapper.Map<List<SpaceDetailsDto>>(spaces);
        return new SpaceResponseDto{ Spaces = spacesDtos};
    }

    public async Task<SpaceDetailsDto> GetSpaceByIdAsync(int id)
    {
        var space = await _spaceRepository.GetSpaceByIdAsync(id);
        return _mapper.Map<SpaceDetailsDto>(space);
    }

    public async Task<SpaceDetailsDto> CreateSpaceAsync(AddSpaceDto spaceDto)
    {
        var space = _mapper.Map<Space>(spaceDto);
        if(spaceDto.ImageUrl != null)
            space.ImageUrl = await _photoService.UploadImageAsync(spaceDto.ImageUrl);
        
        var createdSpace = await _spaceRepository.AddSpaceAsync(space, spaceDto.AmenityIds);
        return _mapper.Map<SpaceDetailsDto>(createdSpace);
    }

    public async Task UpdateSpaceAsync(EditSpaceDto spaceDto)
    {
        var spaceDb = await _spaceRepository.GetSpaceByIdAsync(spaceDto.Id);
        if (spaceDb == null)
            throw new KeyNotFoundException("Space not found");
        
        _mapper.Map(spaceDto, spaceDb);


        if (!string.IsNullOrEmpty(spaceDto.ImageUrl))
            spaceDb.ImageUrl = await _photoService.UploadImageAsync(spaceDto.ImageUrl);
        
        await _spaceRepository.UpdateSpaceAsync(spaceDb, spaceDto.AmenityIds);
    }

    public async Task DeleteSpaceAsync(int id)
    {
        await _spaceRepository.DeleteSpaceAsync(id);
    }
}