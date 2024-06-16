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

                // get orderHeader ready for testing
                BO_OrderHeader orderHeaderBo = mapper.Map<BO_OrderHeader>(orderHeader);

                orderHeaderBo.Id = orderHeader.Id;
                // I don't allow the database to have a orderHeader without knowing who or when it was created
                orderHeaderBo.CreatedBy = orderHeader.CreatedBy;
                orderHeaderBo.CreatedOn = orderHeader.CreatedOn;

                if (orderHeaderBo.IsValid)
                {
                    orderHeader = mapper.Map<OrderHeader>(orderHeaderBo);

                    await orderRepository.UpdateOrderHeaderAsync(orderHeader);

                    OrderHeaderResponse response = mapper.Map<OrderHeaderResponse>(orderHeaderBo);

                    result = Ok(response);
                }
                else
                {
                    OrderHeaderResponse response = mapper.Map<OrderHeaderResponse>(orderHeaderBo);

                    result = BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
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