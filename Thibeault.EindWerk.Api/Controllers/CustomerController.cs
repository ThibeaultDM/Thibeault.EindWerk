using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Api.Models.Response;
using Thibeault.EindWerk.DataLayer;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Services.Rules.RulesFramework.BusinessObjects;

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
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer input)
        {
            // should an invalid customer have been created it will have a tracking Id of "K0"
            // No need to have it keep taking up space
            Customer customer = await repository.GetCustomerByTrackingNumber("K0");

            if (customer == null)
            {
                customer = await repository.AddCustomer();
            }

            // get that in the object for testing
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
    }
}