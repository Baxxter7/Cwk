using Cwk.Domain.Enums;

namespace Cwk.Domain.DTOs.Response;

public class ReservationDetailsDto
{
    public int Id { get; set; }
    public int SpaceId { get; set; }
    public string SpaceName { get; set; } = string.Empty;
    public string SpaceLocation { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int QuantityHours { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public decimal PricePerHour { get; set; }
}
