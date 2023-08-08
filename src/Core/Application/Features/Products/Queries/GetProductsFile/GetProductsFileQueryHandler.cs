using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular.Application.Features.Products.Queries.GetProductsFile
{
    public class GetProductsFileQueryHandler : IRequestHandler<GetProductsFileQuery, ProductsFileVm>
    {
        private readonly IApplicationDbContext context;
        private readonly ICsvFileBuilder fileBuilder;
        private readonly IMapper mapper;
        private readonly IDateTime dateTime;

        public GetProductsFileQueryHandler(
            IApplicationDbContext context,
            ICsvFileBuilder fileBuilder,
            IMapper mapper,
            IDateTime dateTime)
        {
            this.context = context;
            this.fileBuilder = fileBuilder;
            this.mapper = mapper;
            this.dateTime = dateTime;
        }

        public async Task<ProductsFileVm> Handle(
            GetProductsFileQuery request,
            CancellationToken cancellationToken)
        {
            var records = await context.Products
                .ProjectTo<ProductRecordDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var fileContent = fileBuilder.BuildProductsFile(records);

            var vm = new ProductsFileVm
            {
                Content = fileContent,
                ContentType = "text/csv",
                FileName = $"{dateTime.Now:yyyy-MM-dd}-Products.csv",
            };

            return vm;
        }
    }
}
