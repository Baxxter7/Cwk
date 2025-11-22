using Cwk.Domain.Enums;

namespace Cwk.Domain.DTOs.Request;

public class UpdateReservationRequestDto
{
    public int  Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ReservationStatus Status { get; set; }
}
