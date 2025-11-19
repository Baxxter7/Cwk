using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;

namespace Cwk.Business.Interfaces;

public interface ISpaceService
{ 
    Task<SpaceResponseDto> GetAllSpacesAsync();
    Task<SpaceDetailsDto> GetSpaceByIdAsync(int id);
    Task<SpaceDetailsDto> CreateSpaceAsync(AddSpaceDto spaceDto);
    Task UpdateSpaceAsync(EditSpaceDto spaceDto);
    Task DeleteSpaceAsync(int id);

}