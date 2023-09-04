using System.Collections.Generic;
namespace NgNetCore.Domain.Core;

public record Territory
{

    public required string TerritoryId { get; set; }

    public required string TerritoryDescription { get; set; }

    public required int RegionId { get; set; }

    public required Region Region { get; set; }

    public ICollection<EmployeeTerritory> EmployeeTerritories { get; private set; } = new List<EmployeeTerritory>();
}
