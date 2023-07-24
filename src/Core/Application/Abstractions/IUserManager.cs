using System;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Common.Models;

namespace AspNetCoreAngular.Application.Abstractions
{
    public interface IUserManager
    {
        Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(Guid userId);
    }
}
