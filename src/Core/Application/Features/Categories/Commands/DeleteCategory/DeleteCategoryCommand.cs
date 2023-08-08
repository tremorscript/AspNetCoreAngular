using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Exceptions;
using AspNetCoreAngular.Domain.Entities;
using MediatR;

namespace AspNetCoreAngular.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
        {
            private readonly IApplicationDbContext context;

            public DeleteCategoryCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(
                DeleteCategoryCommand request,
                CancellationToken cancellationToken)
            {
                var entity = await context.Categories.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken)
                                    ?? throw new NotFoundException(nameof(Category), request.Id);
                context.Categories.Remove(entity);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
