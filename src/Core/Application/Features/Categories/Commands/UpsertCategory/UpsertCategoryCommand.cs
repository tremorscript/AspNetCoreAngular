using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Domain.Entities;
using MediatR;

namespace AspNetCoreAngular.Application.Features.Categories.Commands.UpsertCategory
{
    public class UpsertCategoryCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCategoryCommand, int>
        {
            private readonly IApplicationDbContext context;

            public UpsertCategoryCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(
                UpsertCategoryCommand request,
                CancellationToken cancellationToken)
            {
                Category entity;

                if (request.Id.HasValue)
                {
                    entity = await context.Categories.FindAsync(new object[] { request.Id.Value }, cancellationToken: cancellationToken);
                }
                else
                {
                    entity = new Category();

                    context.Categories.Add(entity);
                }

                entity.CategoryName = request.Name;
                entity.Description = request.Description;
                entity.Picture = request.Picture;

                await context.SaveChangesAsync(cancellationToken);

                return entity.CategoryId;
            }
        }
    }
}
