// <copyright file="ContactUsConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Persistence.Configurations
{
    using AspNetCoreAngular.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ContactUsConfiguration : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Message).IsRequired().HasMaxLength(1024);
        }
    }
}
