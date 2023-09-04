using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace NgNetCore.Domain.Core;

public record Customer
{

    public required string CustomerId { get; init; }

    public string CompanyName { get; init; } = String.Empty;

    public required Address address { get; init; }

    public string ContactTitle { get; init; } = String.Empty;

    public required string ContactName { get; init; }

    public required string Phone { get; init; }

    public string Fax { get; init; } = String.Empty;

    public IEnumerable<Order> Orders { get; init; } = new List<Order>();
}
