using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using MediatR;

namespace AspNetCoreAngular.Application.Features.System.Commands.SeedLocalizationData
{
    public class LocalizationDataSeederCommandHandler
        : IRequestHandler<LocalizationDataSeederCommand>
    {
        private readonly ILocalizationDbContext context;

        public LocalizationDataSeederCommandHandler(ILocalizationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(
            LocalizationDataSeederCommand request,
            CancellationToken cancellationToken)
        {
            var seeder = new LocalizationDataSeeder(context);

            await seeder.SeedAllAsync(request.ContentRoot, cancellationToken);

            return Unit.Value;
        }
    }
}
