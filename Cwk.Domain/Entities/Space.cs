using Cwk.Domain.Enums;

namespace Cwk.Domain.Entities;

public class Space
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Location { get; set; } = string.Empty;
    public decimal PricePerHour { get; set; }
    public bool IsAvailable { get; set; }
    public string? ImageUrl { get; set; }
    public SpaceType SpaceType { get; set; }
    public SpaceStatus Status { get; set; } = SpaceStatus.Available;
}