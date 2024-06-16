using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Api.Models.Response;
using Thibeault.EindWerk.DataLayer;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Services.Rules.BusinessObjects;

namespace Thibeault.EindWerk.Api.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    [Authorize]
    public class OrderHeaderController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderHeaderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IStockActionRepository actionRepository;
        private readonly IMapper mapper;

        public OrderHeaderController(ICustomerRepository customerRepository, IOrderHeaderRepository orderRepository, IProductRepository productRepository, IStockActionRepository actionRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.actionRepository = actionRepository;
            this.mapper = mapper;
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> Index()
        {
            ObjectResult result;
            try
            {
                List<OrderHeader> orderHeaders = await orderRepository.GetOrderHeadersAsync();

                result = Ok(orderHeaders);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }
            return result;
        }

        [HttpPatch("GetOrderByTrackingNumber")]
        public async Task<IActionResult> GetOrderByTrackingNumberAsync([FromBody] int input)
        {
            ObjectResult result;

            try
            {
                OrderHeader orderHeader = await orderRepository.GetOrderHeaderByTrackingNumberAsync(input);

                OrderHeaderResponse response = mapper.Map<OrderHeaderResponse>(orderHeader);

                result = Ok(response);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpDelete("DeleteOrderHeaderByTrackingNumber")]
        public async Task<IActionResult> DeleteOrderHeaderByTrackingNumberAsync([FromBody] int input)
        {
            ObjectResult result;

            try
            {
                OrderHeader orderHeader = await orderRepository.GetOrderHeaderByTrackingNumberAsync(input);

                await orderRepository.DeleteOrderHeaderAsync(input);

                result = Ok("Order is deleted");
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpPost("AddOrderHeader")]
        public async Task<IActionResult> AddOrderHeaderAsync([FromBody] AddOrderHeader input)
        {
            ObjectResult result;

            try
            {
                Customer customer = await customerRepository.GetCustomerByTrackingNumberAsync(input.CustomerTrackingNumber);
                OrderHeader orderHeader = await orderRepository.CreateOrderHeaderAsync(customer);

                orderHeader.TrackingNumber = orderHeader.Id;
                orderHeader.Status = Objects.Enums.Status.New;

                foreach (StockActionResponseForOrderHeader stockActionToCheck in input.StockActions)
                {
                    StockAction stockAction = await AddStockActionToProductAsync(stockActionToCheck);
                    stockAction.OrderHeader = orderHeader;

                    orderHeader.StockActions.Add(stockAction);
                }

                await orderRepository.UpdateOrderHeaderAsync(orderHeader);

                OrderHeaderResponse response = mapper.Map<OrderHeaderResponse>(orderHeader);

                result = Ok(response);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }
        private async Task<StockAction> AddStockActionToProductAsync(StockActionResponseForOrderHeader input)
        {

            try
            {
                StockAction stockAction = new()
                {
                    Action = Objects.Enums.Action.Reserved,
                    Amount = input.Amount
                };

                Product product = await productRepository.GetProductBySerialNumberAsync(input.ProductSerialNumber);

                BO_Product productBo = mapper.Map<BO_Product>(product);

                productBo.StockActions.Add(stockAction);

                if (productBo.IsValid)
                {
                    product.Reserved = productBo.Reserved;
                    stockAction.Product = product;
                    await actionRepository.ProductAddNewStockActionAsync(product, stockAction);

                    return stockAction;
                }
                else
                {
                    throw new Exception($"Product {input.ProductSerialNumber} does not have enough stock to reserve {input.Amount}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPut("ChangeOrderHeaderCustomer")]
        public async Task<IActionResult> ChangeOrderHeaderCustomer([FromBody] ChangeCustomerOrderHeader input)
        {
            ObjectResult result;

            try
            {
                OrderHeader orderHeader = await orderRepository.GetOrderHeaderByTrackingNumberAsync(input.OrderHeaderTrackingNumber);

                Customer customer = await customerRepository.GetCustomerByTrackingNumberAsync(input.CustomerTrackingNumber);

                orderHeader.Customer = customer;

                await orderRepository.UpdateOrderHeaderAsync(orderHeader);

                result = Ok(orderHeader);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }
    }
}