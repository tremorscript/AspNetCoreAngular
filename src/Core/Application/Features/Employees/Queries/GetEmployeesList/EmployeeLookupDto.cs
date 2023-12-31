﻿using AspNetCoreAngular.Application.Common.Mappings;
using AspNetCoreAngular.Domain.Entities;
using AutoMapper;

namespace AspNetCoreAngular.Application.Features.Employees.Queries.GetEmployeesList
{
    public class EmployeeLookupDto : IMapFrom<Employee>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Extension { get; set; }

        public void Mapping(Profile profile)
        {
            profile
                .CreateMap<Employee, EmployeeLookupDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.LastName + ", " + s.FirstName))
                .ForMember(d => d.Position, opt => opt.MapFrom(s => s.Title));
        }
    }
}
