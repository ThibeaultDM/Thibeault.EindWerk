using AutoMapper;
using Thibeault.Example.Api.Models.Input;
using Thibeault.Example.Api.Models.Response;
using Thibeault.Example.Objects;
using Thibeault.Example.Objects.DataObjects;
using Thibeault.Example.Services.Rules.BusinessObjects;

namespace Thibeault.Example.Api.Helper
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
            CreateMap<OrderHeader, OrderHeaderResponse>();
            CreateMap<OrderHeader, BO_OrderHeader>().ReverseMap();
        }
    }
}