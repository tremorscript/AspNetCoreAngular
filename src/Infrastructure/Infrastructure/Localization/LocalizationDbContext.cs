// <copyright file="LocalizationDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Domain.Entities.Localization;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular.Infrastructure.Localization
{
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
