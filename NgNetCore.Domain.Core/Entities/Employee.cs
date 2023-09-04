using System;
using System.Collections.Generic;

namespace NgNetCore.Domain.Core;

public record Employee : AuditableEntity
{
    public required int EmployeeId { get; init; }

    public required Guid UserId { get; init; }

    public required string LastName { get; init; }

    public required string FirstName { get; init; }

    public required string Title { get; init; }

    public string TitleOfCourtesy { get; init; } = String.Empty;

    public required DateTime BirthDate { get; init; }

    public required DateTime HireDate { get; init; }

    public required Address Address { get; init; }

    public string HomePhone { get; init; } = String.Empty;

    public string Extension { get; init; } = String.Empty;

    public string Notes { get; init; } = String.Empty;

    public byte[]? Photo { get; init; }

    public string PhotoPath { get; init; } = string.Empty;

    public int? ReportsTo { get; init; }

    public Employee? Manager { get; init; }

    public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; init; } = new List<EmployeeTerritory>();

    public IEnumerable<Employee> DirectReports { get; init; } = new List<Employee>();

    public IEnumerable<Order> Orders { get; init; } = new List<Order>();
}
