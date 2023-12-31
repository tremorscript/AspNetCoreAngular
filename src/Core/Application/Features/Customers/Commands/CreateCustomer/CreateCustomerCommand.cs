﻿using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Domain.Entities;
using MediatR;

namespace AspNetCoreAngular.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string Country { get; set; }

        public string Fax { get; set; }

        public string Phone { get; set; }

        public string PostalCode { get; set; }

        public string Region { get; set; }

        public class Handler : IRequestHandler<CreateCustomerCommand>
        {
            private readonly IApplicationDbContext context;
            private readonly IMediator mediator;

            public Handler(IApplicationDbContext context, IMediator mediator)
            {
                this.context = context;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(
                CreateCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var entity = new Customer
                {
                    CustomerId = request.Id,
                    Address = request.Address,
                    City = request.City,
                    CompanyName = request.CompanyName,
                    ContactName = request.ContactName,
                    ContactTitle = request.ContactTitle,
                    Country = request.Country,
                    Fax = request.Fax,
                    Phone = request.Phone,
                    PostalCode = request.PostalCode,
                };

                context.Customers.Add(entity);

                await context.SaveChangesAsync(cancellationToken);

                await mediator.Publish(
                    new CustomerCreated { CustomerId = entity.CustomerId },
                    cancellationToken);

                return Unit.Value;
            }
        }
    }
}
