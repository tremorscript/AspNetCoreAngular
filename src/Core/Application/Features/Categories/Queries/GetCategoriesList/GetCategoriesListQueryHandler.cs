using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler
        : IRequestHandler<GetCategoriesListQuery, CategoriesListVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCategoriesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CategoriesListVm> Handle(
            GetCategoriesListQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await context.Categories
                .ProjectTo<CategoryDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CategoriesListVm { Categories = categories, Count = categories.Count };

            return vm;
        }
    }
}
