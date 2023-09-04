using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular.Application.Features.System.Commands.SeedWebData
{
    internal static class EmployeeExtensions
    {
        public static Employee AddEmployeeTerritories(
            this Employee employee,
            params EmployeeTerritory[] employeeTerritories)
        {
            foreach (var employeeTerritory in employeeTerritories)
            {
                employee.EmployeeTerritories.Add(employeeTerritory);
            }

            return employee;
        }
    }
}
