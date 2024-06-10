using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Api.Models.Response;
using Thibeault.EindWerk.DataLayer;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Services.Rules.BusinessObjects;

namespace Thibeault.EindWerk.Api.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository repository;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<Customer> customers = await repository.GetCustomers();

                List<CreatedCustomer> respons = mapper.Map<List<CreatedCustomer>>(customers);

                return Ok(respons);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CreateCustomer")]
        // TODO look at tracking bug
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomer input)
        {
            Customer customer = await repository.AddCustomer();

            // get customer ready for testing
            BO_Customer customerBo = mapper.Map<BO_Customer>(input);
            customerBo.Id = customer.Id;

            if (customerBo.IsValid)
            {
                customer.TrackingNumber = customerBo.TrackingNumber;

                await repository.UpdateCustomer(customer);

                CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);

                return Ok(response);
            }
            else
            {
                CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);

                return BadRequest(response);
            }
        }

        [HttpPut("UpdateCustomer")]
        // TODO look at tracking bug
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] CreatedCustomer input)
        {
            Customer customer = await repository.GetCustomerByTrackingNumber(input.TrackingNumber);

            if (customer == null)
            {
                return BadRequest("Customer not found");
            }

            customer = mapper.Map<Customer>(input);  

            // get that in the object for testing
            BO_Customer customerBo = mapper.Map<BO_Customer>(customer);

            if (customerBo.IsValid)
            {
                await repository.UpdateCustomer(customer);

                CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);

                return Ok(response);
            }
            else
            {
                CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);

                return BadRequest(response);
            }
        }
    }
}