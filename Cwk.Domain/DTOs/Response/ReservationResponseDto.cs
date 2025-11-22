namespace Cwk.Domain.DTOs.Response;

public class ReservationResponseDto
{
    public List<ReservationDetailsDto> Reservations { get; set; } = new List<ReservationDetailsDto>();
    public int TotalCount { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
