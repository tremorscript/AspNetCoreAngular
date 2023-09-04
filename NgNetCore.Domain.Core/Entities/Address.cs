namespace NgNetCore.Domain.Core;

public record Address
{
    public required string AddressLine1 { get; init; }

    public string AddressLine2 { get; init; } = String.Empty;

    public required string City { get; init; }

    public string Region { get; init; } = String.Empty;

    public required string PostalCode { get; init; }

    public required string Country { get; init; }

}
