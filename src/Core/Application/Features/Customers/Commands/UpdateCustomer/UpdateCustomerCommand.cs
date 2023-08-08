using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Exceptions;
using AspNetCoreAngular.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest
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

        public class Handler : IRequestHandler<UpdateCustomerCommand>
        {
            private readonly IApplicationDbContext context;

            public Handler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(
                UpdateCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var entity = await context.Customers.SingleOrDefaultAsync(
                    c => c.CustomerId == request.Id,
                    cancellationToken) ?? throw new NotFoundException(nameof(Customer), request.Id);
                entity.Address = request.Address;
                entity.City = request.City;
                entity.CompanyName = request.CompanyName;
                entity.ContactName = request.ContactName;
                entity.ContactTitle = request.ContactTitle;
                entity.Country = request.Country;
                entity.Fax = request.Fax;
                entity.Phone = request.Phone;
                entity.PostalCode = request.PostalCode;

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
