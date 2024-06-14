using AutoMapper;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Api.Models.Response;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Services.Rules.BusinessObjects;

namespace Thibeault.EindWerk.Api.Helper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // TODO look at custom mapping again
            // how to only map properties that they share

            // Customer
            CreateMap<BO_Customer, Customer>().ReverseMap();
            CreateMap<CreateCustomer, BO_Customer>();
            CreateMap<CreatedCustomer, BO_Customer>().ReverseMap();
            CreateMap<CreatedCustomer, Customer>().ReverseMap();

            // Product
            CreateMap<BO_Product, Product>().ReverseMap();
            CreateMap<AddProduct, Product>();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<Product, ProductResponseForStockAction>();

            // Address
            CreateMap<Address, AddressDto>().ReverseMap();

            // StockAction
            CreateMap<StockActionDto, BO_StockAction>();
            CreateMap<StockAction, BO_StockAction>();
            CreateMap<BO_StockAction, StockActionResponse>();
        }
    }
}