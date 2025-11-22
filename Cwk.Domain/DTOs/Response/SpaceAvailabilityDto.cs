namespace Cwk.Domain.DTOs.Response;

public class SpaceAvailabilityDto
{
    public int SpaceId { get; set; }
    public bool IsAvailable { get; set;  }
    public List<TimeSlot> AvailableSlots { get; set; }
    public List<ReservationDetailsDto> ExistingReservations { get; set; } = new List<ReservationDetailsDto>();
}
