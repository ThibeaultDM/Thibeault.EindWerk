using AutoMapper;
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
                List<Customer> customers = await repository.GetCustomersAsync();

                List<CreatedCustomer> respons = mapper.Map<List<CreatedCustomer>>(customers);

                return Ok(respons);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("GetCustomerByTrackingNumberAsync")]
        public async Task<IActionResult> GetCustomerTrackingNumberAsync([FromBody] string input)
        {
            ObjectResult result;

            try
            {
                Customer customer = await repository.GetCustomerByTrackingNumberAsync(input);

                CreatedCustomer respons = mapper.Map<CreatedCustomer>(customer);

                if (respons == null)
                {
                    result = BadRequest("Customer not found");
                }
                else
                {
                    result = Ok(respons);
                }

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCustomerByTrackingNumber")]
        public async Task<IActionResult> DeleteCustomerTrackingNumberAsync([FromBody] string input)
        {
            ObjectResult result;

            try
            {
                bool IsDeleted = await repository.DeleteCustomerAsync(input);

                if (IsDeleted)
                {
                    result = Ok("Customer is deleted");
                }
                else
                {
                    result = BadRequest("Customer not found");
                }

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomer input)
        {
            try
            {
                ObjectResult result;

                Customer customer = await repository.CreateCustomerAsync();

                // get customer ready for testing
                BO_Customer customerBo = mapper.Map<BO_Customer>(input);

                customerBo.Id = customer.Id;
                // I don't allow the database to have a customer without knowing who or when it was created
                customerBo.CreatedBy = customer.CreatedBy;
                customerBo.CreatedOn = customer.CreatedOn;

                if (customerBo.IsValid)
                {
                    customer = mapper.Map<Customer>(customerBo); ;

                    await repository.UpdateCustomer(customer);

                    CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);

                    result = Ok(response);
                }
                else
                {
                    CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);

                    result = BadRequest(response);
                }

                return result;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomer input)
        {
            try
            {
                ObjectResult result;
                Customer customer = await repository.GetCustomerByTrackingNumberAsync(input.TrackingNumber);

                if (customer == null)
                {
                    result = BadRequest("Customer not found");
                }
                else
                {
                    // get that in the object for testing
                    BO_Customer customerBo = mapper.Map<BO_Customer>(customer);

                    customerBo.FullName = input.FullName;
                    customerBo.Email = input.Email;

                    customerBo.Address = mapper.Map<Address>(input.Address);

                    if (customerBo.IsValid)
                    {
                        customer = mapper.Map<Customer>(customerBo);

                        await repository.UpdateCustomer(customer);

                        CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);

                        result = Ok(response);
                    }
                    else
                    {
                        CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);

                        result = BadRequest(response);
                    }

                }

                return result;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}