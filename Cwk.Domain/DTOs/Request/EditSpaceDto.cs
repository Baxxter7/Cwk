using Cwk.Domain.Enums;

namespace Cwk.Domain.DTOs.Request;

public class EditSpaceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Location { get; set; } = string.Empty;
    public decimal PricePerHour { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public SpaceType SpaceType { get; set; }
    public SpaceStatus Status { get; set; }
    public List<int> AmenityIds { get; set; } = [];
}
