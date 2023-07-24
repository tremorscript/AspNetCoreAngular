﻿using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Exceptions;
using AspNetCoreAngular.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AspNetCoreAngular.Application.Features.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDetailVm> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            return _mapper.Map<CustomerDetailVm>(entity);
        }
    }
}
