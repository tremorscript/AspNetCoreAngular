﻿// <copyright file="ShipperConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Persistence.Configurations
{
    using AspNetCoreAngular.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.Property(e => e.ShipperId).HasColumnName("ShipperID");

            builder.Property(e => e.CompanyName).IsRequired().HasMaxLength(40);

            builder.Property(e => e.Phone).HasMaxLength(24);
        }
    }
}
