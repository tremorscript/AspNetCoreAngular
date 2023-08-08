using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular.Application.Features.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailQuery : IRequest<EmployeeDetailVm>
    {
        public int Id { get; set; }

        public class GetEmployeeDetailQueryHandler
            : IRequestHandler<GetEmployeeDetailQuery, EmployeeDetailVm>
        {
            private readonly IApplicationDbContext context;
            private readonly IMapper mapper;

            public GetEmployeeDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<EmployeeDetailVm> Handle(
                GetEmployeeDetailQuery request,
                CancellationToken cancellationToken)
            {
                var vm = await context.Employees
                    .Where(e => e.EmployeeId == request.Id)
                    .ProjectTo<EmployeeDetailVm>(mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

                return vm;
            }
        }
    }
}
