using System.Collections.Generic;
namespace NgNetCore.Domain.Core;

public record Supplier
{
    public required int SupplierId { get; init; }

    public required string CompanyName { get; init; }

    public required string ContactName { get; init; }

    public required string ContactTitle { get; init; }

    public required Address SupplierAddress { get; init; }

    public string Phone { get; init; } = String.Empty;

    public string Fax { get; init; } = String.Empty;

    public string HomePage { get; init; } = String.Empty;

    public IEnumerable<Product> Products { get; private init; } = new List<Product>();
}
