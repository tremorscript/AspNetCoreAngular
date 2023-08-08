// <copyright file="EFStringLocalizerFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Localization.EFLocalizer
{
    using System;
    using AspNetCoreAngular.Application.Abstractions;
    using Microsoft.Extensions.Localization;

    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ILocalizationDbContext context;

        public EFStringLocalizerFactory(ILocalizationDbContext context)
        {
            this.context = context;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new EFStringLocalizer(this.context);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new EFStringLocalizer(this.context);
        }
    }
}
