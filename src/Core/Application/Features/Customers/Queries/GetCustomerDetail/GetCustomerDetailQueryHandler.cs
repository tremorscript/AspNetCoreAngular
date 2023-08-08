using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Exceptions;
using AspNetCoreAngular.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AspNetCoreAngular.Application.Features.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler
        : IRequestHandler<GetCustomerDetailQuery, CustomerDetailVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCustomerDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CustomerDetailVm> Handle(
            GetCustomerDetailQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await context.Customers.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken)
                                        ?? throw new NotFoundException(nameof(Customer), request.Id);
            return mapper.Map<CustomerDetailVm>(entity);
        }
    }
}
