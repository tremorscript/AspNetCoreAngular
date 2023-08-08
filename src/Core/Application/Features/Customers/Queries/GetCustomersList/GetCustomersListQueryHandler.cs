using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular.Application.Features.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQueryHandler
        : IRequestHandler<GetCustomersListQuery, CustomersListVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCustomersListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CustomersListVm> Handle(
            GetCustomersListQuery request,
            CancellationToken cancellationToken)
        {
            var customers = await context.Customers
                .ProjectTo<CustomerLookupDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CustomersListVm { Customers = customers };

            return vm;
        }
    }
}
