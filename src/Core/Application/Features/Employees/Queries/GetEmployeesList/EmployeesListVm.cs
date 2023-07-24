using System.Collections.Generic;

namespace AspNetCoreAngular.Application.Features.Employees.Queries.GetEmployeesList
{
    public class EmployeesListVm
    {
        public IList<EmployeeLookupDto> Employees { get; set; }
    }
}
