using System.Collections.Generic;

namespace NgNetCore.Domain.Core;

public record Category
{
    public required int CategoryId { get; init; }

    public required string CategoryName { get; init; }

    public string Description { get; init; } = string.Empty;

    public byte[]? Picture { get; set; }

    public IEnumerable<Product> Products { get; private init; } = new List<Product>();
}
