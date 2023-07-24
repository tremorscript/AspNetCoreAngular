using MediatR;

namespace AspNetCoreAngular.Application.Features.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery : IRequest<CustomerDetailVm>
    {
        public string Id { get; set; }
    }
}
