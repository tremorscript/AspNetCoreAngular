using MediatR;

namespace AspNetCoreAngular.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<CategoriesListVm>
    {
    }
}
