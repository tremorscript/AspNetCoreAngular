namespace NgNetCore.Domain.Core;

public record OrderDetail : AuditableEntity
{
    public required int OrderId { get; set; }

    public required int ProductId { get; set; }

    public required decimal UnitPrice { get; set; }

    public required short Quantity { get; set; }

    public required float Discount { get; set; }

    public required Order Order { get; set; }

    public required Product Product { get; set; }
}
