﻿using AspNetCoreAngular.Application.Features.Products.Queries.GetProductsFile;
using CsvHelper.Configuration;

namespace AspNetCoreAngular.Infrastructure.Files
{
    public sealed class ProductFileRecordMap : ClassMap<ProductRecordDto>
    {
        public ProductFileRecordMap()
        {
            Map(m => m.UnitPrice).Name("Unit Price").Convert(c => (c.Value.UnitPrice ?? 0).ToString("C"));
        }
    }
}
