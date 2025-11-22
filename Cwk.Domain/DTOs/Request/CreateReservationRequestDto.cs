namespace Cwk.Domain.DTOs.Request;

public class CreateReservationRequestDto
{
    public int SpaceId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int UserId { get; set; }
}
