// <copyright file="LocalizationDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Localization
{
    using AspNetCoreAngular.Application.Abstractions;
    using AspNetCoreAngular.Domain.Entities.Localization;
    using Microsoft.EntityFrameworkCore;

    public class LocalizationDbContext : DbContext, ILocalizationDbContext
    {
        public LocalizationDbContext(DbContextOptions<LocalizationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Culture> Cultures { get; set; }

        public DbSet<Resource> Resources { get; set; }
    }
}
