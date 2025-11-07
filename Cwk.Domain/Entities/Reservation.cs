using Cwk.Domain.Enums;

namespace Cwk.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int SpaceId { get; set; }
    public Space Space { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int QuantityHours { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Pending;
}