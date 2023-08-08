using AspNetCoreAngular.Application.Common.Mappings;
using AspNetCoreAngular.Domain.Entities;
using AutoMapper;

namespace AspNetCoreAngular.Application.Features.Customers.Queries.GetCustomersList
{
    public class CustomerLookupDto : IMapFrom<Customer>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile
                .CreateMap<Customer, CustomerLookupDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CustomerId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CompanyName));
        }
    }
}
