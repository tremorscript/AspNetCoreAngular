using System.Collections.Generic;
using AspNetCoreAngular.Application.Features.Products.Queries.GetProductsFile;

namespace AspNetCoreAngular.Application.Abstractions
{
    public interface ICsvFileBuilder
    {
        byte[] BuildProductsFile(IEnumerable<ProductRecordDto> records);
    }
}
