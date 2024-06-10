using AutoMapper;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Api.Models.Response;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Services.Rules.BusinessObjects;

namespace Thibeault.EindWerk.Api.Models
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Customer
            CreateMap<BO_Customer, Customer>().ReverseMap();
            CreateMap<CreateCustomer, BO_Customer>().ReverseMap();
            CreateMap<CreatedCustomer, BO_Customer>().ReverseMap();
            CreateMap<CreatedCustomer, Customer>().ReverseMap();

            // Product
            CreateMap<BO_Product, Product>().ReverseMap();

            // Address
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}