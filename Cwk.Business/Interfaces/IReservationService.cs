using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;

namespace Cwk.Business.Interfaces;

public interface IReservationService
{
    Task<ReservationResponseDto> GetReservationsAsync(ReservationQueryDto query);
    Task<ReservationDetailsDto?> GetReservationByIdAsync(int id);
    Task<ReservationDetailsDto> CreateReservationAsync(CreateReservationRequestDto request);
    Task<ReservationDetailsDto?> UpdateReservationAsync(UpdateReservationRequestDto request);
    Task<bool> DeleteReservationAsync(int id);
    Task<SpaceAvailabilityDto?> CheckSpaceAvilabilityAsync(int spaceId, DateTime startTime, DateTime endTime);
    Task<bool> ConfirmReservationAsync(int reservationId);
    Task<bool> CancelReservationAsync(int reservationId);
}
