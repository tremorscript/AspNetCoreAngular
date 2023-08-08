using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Exceptions;
using AspNetCoreAngular.Domain.Entities;
using MediatR;

namespace AspNetCoreAngular.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await context.Products.FindAsync(new object[] { request.ProductId }, cancellationToken: cancellationToken)
                         ?? throw new NotFoundException(nameof(Product), request.ProductId);
            entity.ProductId = request.ProductId;
            entity.ProductName = request.ProductName;
            entity.CategoryId = request.CategoryId;
            entity.SupplierId = request.SupplierId;
            entity.UnitPrice = request.UnitPrice;
            entity.Discontinued = request.Discontinued;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
