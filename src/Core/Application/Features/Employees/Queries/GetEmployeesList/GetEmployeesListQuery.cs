using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular.Application.Features.Employees.Queries.GetEmployeesList
{
    public class GetEmployeesListQuery : IRequest<EmployeesListVm>
    {
        public class GetEmployeesListQueryHandler
            : IRequestHandler<GetEmployeesListQuery, EmployeesListVm>
        {
            private readonly IApplicationDbContext context;
            private readonly IMapper mapper;

            public GetEmployeesListQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<EmployeesListVm> Handle(
                GetEmployeesListQuery request,
                CancellationToken cancellationToken)
            {
                var employees = await context.Employees
                    .ProjectTo<EmployeeLookupDto>(mapper.ConfigurationProvider)
                    .OrderBy(e => e.Name)
                    .ToListAsync(cancellationToken);

                var vm = new EmployeesListVm { Employees = employees };

                return vm;
            }
        }
    }
}
