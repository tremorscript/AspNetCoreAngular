using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using MediatR;

namespace AspNetCoreAngular.Application.Features.System.Commands.SeedWebData
{
    public class SeedSampleDataCommandHandler : IRequestHandler<WebDataSeederCommand>
    {
        private readonly IApplicationDbContext context;

        // private readonly IUserManager _userManager;
        public SeedSampleDataCommandHandler(IApplicationDbContext context)
        {
            this.context = context;

            // _userManager = userManager;
        }

        public async Task<Unit> Handle(
            WebDataSeederCommand request,
            CancellationToken cancellationToken)
        {
            var seeder = new WebDataSeeder(context);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
