using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace AspNetCoreAngular.Application.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger logger;
        private readonly ICurrentUserService currentUserService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            this.logger = logger;
            this.currentUserService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            logger.LogInformation(
                "AspNetCoreAngular Request: {Name} {@UserId} {@Request}",
                name,
                currentUserService.UserId,
                request);

            return Task.CompletedTask;
        }
    }
}
