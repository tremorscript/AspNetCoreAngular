﻿using System.Threading.Tasks;
using AspNetCoreAngular.Application.Features.Categories.Commands.DeleteCategory;
using AspNetCoreAngular.Application.Features.Categories.Commands.UpsertCategory;
using AspNetCoreAngular.Application.Features.Categories.Queries.GetCategoriesList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAngular.Web.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CategoriesListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCategoriesListQuery()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertCategoryCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
