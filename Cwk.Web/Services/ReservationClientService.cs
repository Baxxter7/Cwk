using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;

namespace Cwk.Web.Services;

public class ReservationClientService : IReservationClientService
{
    private readonly IRequestService _requestService;

    public ReservationClientService(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<bool> CancelReservationAsync(int reservationId)
    {
        try
        {
            await _requestService.PutAsync($"api/reservation/{reservationId}/cancel");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<SpaceAvailabilityDto> CheckSpaceAvailabilityAsync(int spaceId, DateTime startTime, DateTime endTime)
    {
        var url = $"api/reservations/availability/{spaceId}?startTime={startTime:yyyy-MM-ddTHH:mm:ss}&endTime={endTime:yyyy-MM-ddTHH:mm:ss}";
        return await _requestService.GetAsync<SpaceAvailabilityDto>(url);
    }


    public async Task<bool> ConfirmReservationAsync(int reservationId)
    {
        try
        {
            await _requestService.PutAsync($"api/reservations/{reservationId}/confirm");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ReservationDetailsDto> CreateReservationAsync(CreateReservationRequestDto request)
    {
        return await _requestService.PostAndReadAsync<ReservationDetailsDto, CreateReservationRequestDto>("api/reservations", request);
    }

    public async Task<ReservationResponseDto> GetReservationsAsync(ReservationQueryDto query)
    {
        var queryString = $"?userId={query.UserId}&spaceId={query.SpaceId}&status={query.Status}&startDate={query.StartDate:yyyy-MM-dd}&endDate={query.EndDate:yyyy-MM-dd}";
        return await _requestService.GetAsync<ReservationResponseDto>($"api/reservations{queryString}");
    }


    public async Task<ReservationResponseDto> GetUserReservationsAsync(int userId)
    {
        return await _requestService.GetAsync<ReservationResponseDto>($"api/reservations?userId={userId}");
    }
}
