using MediatR;

namespace AspNetCoreAngular.Application.Features.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQuery : IRequest<CustomersListVm>
    {
    }
}
