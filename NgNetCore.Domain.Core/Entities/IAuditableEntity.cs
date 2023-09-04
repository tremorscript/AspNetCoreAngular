using System;
namespace NgNetCore.Domain.Core;

public record AuditableEntity
{
    public required string CreatedBy { get; init; }

    public required DateTime Created { get; init; }

    public required string LastModifiedBy { get; init; }

    public required DateTime? LastModified { get; init; }
}
