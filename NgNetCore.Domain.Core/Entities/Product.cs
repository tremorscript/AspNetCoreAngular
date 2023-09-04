namespace NgNetCore.Domain.Core;

public record Product : AuditableEntity
{
    public required int ProductId { get; init; }

    public required string ProductName { get; init; }

    public required int SupplierId { get; init; }

    public required int CategoryId { get; init; }

    public string QuantityPerUnit { get; init; } = String.Empty;

    public required decimal UnitPrice { get; init; }

    public short? UnitsInStock { get; init; }

    public short? UnitsOnOrder { get; init; }

    public short? ReorderLevel { get; init; }

    public bool Discontinued { get; init; }

    public required Category Category { get; init; }

    public required Supplier Supplier { get; init; }

    public IEnumerable<OrderDetail> OrderDetails { get; init; } = new List<OrderDetail>();
}
