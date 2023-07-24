using AspNetCoreAngular.Application.Models;

namespace AspNetCoreAngular.Application.Abstractions
{
    public interface IClientInfoService
    {
        ClientInfo GetClient();
    }
}
