﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Features.Employees.Commands.DeleteEmployee;
using AspNetCoreAngular.Application.Features.Employees.Commands.UpsertEmployee;
using AspNetCoreAngular.Application.Features.Employees.Queries.GetEmployeeDetail;
using AspNetCoreAngular.Application.Features.Employees.Queries.GetEmployeesList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAngular.Web.Controllers
{
    public class EmployeesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeesListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetEmployeesListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeDetailVm>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeDetailQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertEmployeeCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEmployeeCommand { Id = id });

            return NoContent();
        }
    }
}
