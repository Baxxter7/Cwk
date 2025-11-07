namespace Cwk.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string TransactionId { get; set; } = null!;
}