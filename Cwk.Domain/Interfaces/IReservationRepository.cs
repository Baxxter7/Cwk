using Cwk.Domain.Entities;

namespace Cwk.Domain.Interfaces;

public interface IReservationRepository
{
    Task<Reservation> GetByIdAsync(int id);

    Task<List<Reservation>> GetAvailablesAsync(int spaceId, DateTime startTime, DateTime endTime);
    Task<List<Reservation>> GetAllAsync();
    Task<Reservation> AddAsync(Reservation reservation);
    Task<Reservation> UpdateAsync(Reservation reservation);
    Task<List<Reservation>> GetBySpaceIdAsync(int spaceId);
}
