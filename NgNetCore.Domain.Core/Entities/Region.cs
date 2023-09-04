using System.Collections.Generic;
namespace NgNetCore.Domain.Core;

public record Region
{
    public required int RegionId { get; init; }

    public required string RegionDescription { get; init; } = String.Empty;

    public IEnumerable<Territory> Territories { get; init; } = new List<Territory>();
}
