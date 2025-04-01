using UKG.HCM.Application.Entities.BaseTypes;

namespace UKG.HCM.Application.Entities;

public record Person
{
    public required Guid PersonId { get; set; }
    
    public required Name Name { get; set; }
    public required Email? Email { get; set; }
}