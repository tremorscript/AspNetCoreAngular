using System;
using System.Collections.Generic;

namespace NgNetCore.Domain.Core;

public record Order : AuditableEntity
{
    public required int OrderId { get; init; }

    public required string CustomerId { get; init; }

    public int? EmployeeId { get; init; }

    public required DateTime OrderDate { get; init; }

    public DateTime? RequiredDate { get; init; }

    public DateTime? ShippedDate { get; init; }

    public int? ShipVia { get; init; }

    public decimal? Freight { get; init; }

    public string ShipName { get; init; } = String.Empty;

    public required Address ShipAddress { get; init; }

    public required Customer Customer { get; init; }

    public required Employee Employee { get; init; }

    public required Shipper Shipper { get; init; }

    public IEnumerable<OrderDetail> OrderDetails { get; private init; } = new List<OrderDetail>();
}
