// <copyright file="IIdentitySeedData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.STS.Seed
{
    using System;

    public interface IIdentitySeedData
    {
        void Seed(IServiceProvider serviceProvider);
    }
}
