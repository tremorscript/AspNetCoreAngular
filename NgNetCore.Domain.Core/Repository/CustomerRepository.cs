using Ardalis.GuardClauses;
using OneOf;
using OneOf.Types;
using static TryCatchGuardExceptions;

public record CustomerIsNull { }
public record RepositoryIsNull { }

public static class CustomerService
{

    public static OneOf<Customer, CustomerIsNull, RepositoryIsNull> CreateCustomer(Customer customer, ICustomerRepository repository)
    {
        if (ThrowsException(() => Guard.Against.Null(customer))) return new CustomerIsNull();
        if (ThrowsException(() => Guard.Against.Null(repository))) return new RepositoryIsNull();
        return repository.Create(customer);
    }
}
