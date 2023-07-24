using AspNetCoreAngular.Application.Models;

namespace AspNetCoreAngular.Application.Abstractions
{
    public interface IApplicationService
    {
        ApplicationDataViewModel GetApplicationData();
    }
}