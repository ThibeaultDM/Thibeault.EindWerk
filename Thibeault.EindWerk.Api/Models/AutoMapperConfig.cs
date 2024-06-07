using AutoMapper;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Api.Models.Response;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Services.RulesFramework.BusinessObjects;

namespace Thibeault.EindWerk.Api.Models
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Customer
            CreateMap<BO_Customer, Customer>().ReverseMap();
            CreateMap<CreateCustomer, Customer>().ReverseMap(); 
            CreateMap<CreatedCustomer, Customer>().ReverseMap();
            // Product
            CreateMap<BO_Product, Product>().ReverseMap();
        }
    }
}
