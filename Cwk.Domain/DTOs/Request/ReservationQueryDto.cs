using Cwk.Domain.Enums;

namespace Cwk.Domain.DTOs.Request;

public class ReservationQueryDto
{
    public int? SpaceId { get; set; }
    public int? UserId { get; set; }
    public DateTime? StartDate {  get; set; }
    public DateTime? EndDate { get; set; }
    public ReservationStatus Status { get; set; }
}
