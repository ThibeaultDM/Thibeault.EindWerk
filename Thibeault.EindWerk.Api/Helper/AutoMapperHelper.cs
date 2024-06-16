using AutoMapper;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Api.Models.Response;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Services.Rules.BusinessObjects;

namespace Thibeault.EindWerk.Api.Helper
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            // TODO look at custom mapping again
            // how to only map properties that they share

            // Customer
            CreateMap<BO_Customer, Customer>().ReverseMap();
            CreateMap<CreateCustomer, BO_Customer>();
            CreateMap<CreatedCustomer, BO_Customer>().ReverseMap();
            CreateMap<CreatedCustomer, Customer>().ReverseMap();
            CreateMap<Customer, CustomerResponse>();

            // Product
            CreateMap<BO_Product, Product>().ReverseMap();
            CreateMap<AddProduct, BO_Product>();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<Product, ProductResponseForStockAction>();
            CreateMap<Product, ProductResponse>();
            CreateMap<BO_Product, ProductResponse>().ReverseMap();
            CreateMap<BO_Product, ProductDto>().ReverseMap();

            // Addresses
            CreateMap<Address, AddressDto>().ReverseMap();

            // StockAction
            CreateMap<StockActionDto, BO_StockAction>().ReverseMap();
            CreateMap<StockAction, BO_StockAction>().ReverseMap();
            CreateMap<BO_StockAction, StockActionResponse>().ReverseMap();
            CreateMap<StockActionDto, StockAction>().ReverseMap();
            CreateMap<StockAction, StockActionResponse>().ReverseMap();
            CreateMap<StockAction, StockActionResponseForOrderHeader>();

            // OrderHeaders
            CreateMap<OrderHeader, OrderHeaderResponse>().ReverseMap();
        }
    }
}