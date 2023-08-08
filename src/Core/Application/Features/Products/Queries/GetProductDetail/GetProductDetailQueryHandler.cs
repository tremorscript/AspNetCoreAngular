using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Exceptions;
using AspNetCoreAngular.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular.Application.Features.Products.Queries.GetProductDetail
{
    public class GetProductDetailQueryHandler
        : MediatR.IRequestHandler<GetProductDetailQuery, ProductDetailVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetProductDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ProductDetailVm> Handle(
            GetProductDetailQuery request,
            CancellationToken cancellationToken)
        {
            var vm = await context.Products
                .ProjectTo<ProductDetailVm>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.ProductId == request.Id, cancellationToken)
                        ?? throw new NotFoundException(nameof(Product), request.Id);
            return vm;
        }
    }
}
