using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Exceptions;
using AspNetCoreAngular.Domain.Entities;
using MediatR;

namespace AspNetCoreAngular.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteProductCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(
            DeleteProductCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await context.Products.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken)
                         ?? throw new NotFoundException(nameof(Product), request.Id);
            var hasOrders = context.OrderDetails.Any(od => od.ProductId == entity.ProductId);
            if (hasOrders)
            {
                // TODO: Add functional test for this behaviour.
                throw new DeleteFailureException(
                    nameof(Product),
                    request.Id,
                    "There are existing orders associated with this product.");
            }

            context.Products.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
