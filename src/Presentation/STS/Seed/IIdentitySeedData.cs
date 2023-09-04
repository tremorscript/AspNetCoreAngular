// <copyright file="IIdentitySeedData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace AspNetCoreAngular.STS.Seed
{
    public interface IIdentitySeedData
    {
        void Seed(IServiceProvider serviceProvider);
    }
}
