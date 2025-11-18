using Cwk.Domain.Entities;
namespace Cwk.Domain.Interfaces;

public interface ISpaceRepository
{
    Task<Space?>  GetSpaceByIdAsync(int id);
    Task<List<Space>> GetAllSpacesAsync();
    Task<Space> AddSpaceAsync(Space space, List<int> amenityIds);
    Task<Space> UpdateSpaceAsync(Space space, List<int> amenityIds);
    Task DeleteSpaceAsync(int id);
}