using AutoMapper;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Services.RulesFramework.BusinessObjects;

namespace Thibeault.EindWerk.Api.Models
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<BO_Customer, Customer>().ReverseMap();
            CreateMap<BO_Product, Product>().ReverseMap();
        }
    }
}
