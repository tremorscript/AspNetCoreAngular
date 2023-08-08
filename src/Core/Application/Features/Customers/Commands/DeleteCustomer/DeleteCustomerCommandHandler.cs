using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Exceptions;
using AspNetCoreAngular.Domain.Entities;
using MediatR;

namespace AspNetCoreAngular.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteCustomerCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(
            DeleteCustomerCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await context.Customers.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken)
                                    ?? throw new NotFoundException(nameof(Customer), request.Id);
            var hasOrders = context.Orders.Any(o => o.CustomerId == entity.CustomerId);
            if (hasOrders)
            {
                throw new DeleteFailureException(
                    nameof(Customer),
                    request.Id,
                    "There are existing orders associated with this customer.");
            }

            context.Customers.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
