using System.Collections.Generic;

namespace AspNetCoreAngular.Application.Features.Categories.Queries.GetCategoriesList
{
    public class CategoriesListVm
    {
        public IList<CategoryDto> Categories { get; set; }

        public int Count { get; set; }
    }
}
