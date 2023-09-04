using System.Collections.Generic;
namespace NgNetCore.Domain.Core;

public record Shipper
{
    public required int ShipperId { get; init; }

    public required string CompanyName { get; init; }

    public required string Phone { get; init; }

    public IEnumerable<Order> Orders { get; private set; } = new List<Order>();
}
