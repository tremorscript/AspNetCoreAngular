using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using MediatR;

namespace AspNetCoreAngular.Application.Features.System.Commands.SeedLocalizationData
{
    public class LocalizationDataSeederCommand : IRequest
    {
        public string ContentRoot { get; set; }
    }

    public class LocalizationDataSeederCommandHandler : IRequestHandler<LocalizationDataSeederCommand>
    {
        private readonly ILocalizationDbContext _context;

        public LocalizationDataSeederCommandHandler(ILocalizationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(LocalizationDataSeederCommand request, CancellationToken cancellationToken)
        {
            var seeder = new LocalizationDataSeeder(_context);

            await seeder.SeedAllAsync(request.ContentRoot, cancellationToken);

            return Unit.Value;
        }
    }
}
