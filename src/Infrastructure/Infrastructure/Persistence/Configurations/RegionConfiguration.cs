﻿// <copyright file="RegionConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AspNetCoreAngular.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreAngular.Infrastructure.Persistence.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(e => e.RegionId).IsClustered(false);

            builder.Property(e => e.RegionId).HasColumnName("RegionID").ValueGeneratedNever();

            builder.Property(e => e.RegionDescription).IsRequired().HasMaxLength(50);
        }
    }
}
