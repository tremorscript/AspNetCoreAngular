﻿// <copyright file="ProductFileRecordMap.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AspNetCoreAngular.Application.Features.Products.Queries.GetProductsFile;
using CsvHelper.Configuration;

namespace AspNetCoreAngular.Infrastructure.Files
{
    public sealed class ProductFileRecordMap : ClassMap<ProductRecordDto>
    {
        public ProductFileRecordMap()
        {
            this.Map(m => m.UnitPrice)
                .Name("Unit Price")
                .Convert(c => (c.Value.UnitPrice ?? 0).ToString("C"));
        }
    }
}
