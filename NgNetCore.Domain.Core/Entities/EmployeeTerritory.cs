namespace NgNetCore.Domain.Core;

public record EmployeeTerritory
{
    public required int EmployeeId { get; init; }

    public required int TerritoryId { get; init; }

    public required Employee Employee { get; init; }

    public required Territory Territory { get; init; }
}
