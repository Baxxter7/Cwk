namespace Cwk.Domain.DTOs.Request;

public class UpdateAmenityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}