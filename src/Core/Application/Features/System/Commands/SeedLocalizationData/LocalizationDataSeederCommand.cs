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
}
