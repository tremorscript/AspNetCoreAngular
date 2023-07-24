using System;

namespace AspNetCoreAngular.STS.Seed
{
    public interface IIdentitySeedData
    {
        void Seed(IServiceProvider serviceProvider);
    }
}