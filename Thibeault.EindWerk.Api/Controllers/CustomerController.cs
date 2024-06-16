using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Api.Models.Response;
using Thibeault.EindWerk.DataLayer;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Services;
using Thibeault.EindWerk.Services.Rules.BusinessObjects;

namespace Thibeault.EindWerk.Api.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository repository;
        private readonly CustomerService service;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepository repository, CustomerService service, IMapper mapper)
        {
            this.repository = repository;
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> Index()
        {
            ObjectResult result;

            try
            {
                List<Customer> customers = await repository.GetCustomersAsync();

                List<CreatedCustomer> response = mapper.Map<List<CreatedCustomer>>(customers);

                result = Ok(response);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpPatch("GetCustomerByTrackingNumber")]
        public async Task<IActionResult> GetCustomerTrackingNumberAsync([FromBody] string input)
        {
            ObjectResult result;

            try
            {
                Customer customer = await repository.GetCustomerByTrackingNumberAsync(input);

                CreatedCustomer response = mapper.Map<CreatedCustomer>(customer);

                result = Ok(response);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpDelete("DeleteCustomerByTrackingNumber")]
        public async Task<IActionResult> DeleteCustomerTrackingNumberAsync([FromBody] string input)
        {
            ObjectResult result;

            try
            {
                await repository.DeleteCustomerAsync(input);

                result = Ok("Customer is deleted");
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomer input)
        {
            ObjectResult result;

            try
            {
                Customer customer = await repository.CreateCustomerAsync();

                // get customer ready for testing
                BO_Customer customerBo = mapper.Map<BO_Customer>(input);

                customerBo.Id = customer.Id;
                // I don't allow the database to have a customer without knowing who or when it was created
                customerBo.CreatedBy = customer.CreatedBy;
                customerBo.CreatedOn = customer.CreatedOn;

                if (customerBo.IsValid)
                {
                    customer = mapper.Map<Customer>(customerBo);
                    await repository.UpdateCustomerAsync(customer);

                    CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);
                    result = Ok(response);
                }
                else
                {
                    CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);
                    result = BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        // General design: get the object you want to change,
        // execute those changes on a BO to see if the changes are valid,
        // save or throw error
        [HttpPut("UpdateCustomerAsync")]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomer input)
        {
            ObjectResult result;

            try
            {
                Customer customer = await repository.GetCustomerByTrackingNumberAsync(input.TrackingNumber);

                // get that in the object for testing
                BO_Customer customerBo = mapper.Map<BO_Customer>(customer);

                customerBo.FullName = input.FullName;
                customerBo.Email = input.Email;

                customerBo.Address = mapper.Map<Address>(input.Address);

                if (customerBo.IsValid)
                {
                    customer = mapper.Map<Customer>(customerBo);
                    await repository.UpdateCustomerAsync(customer);

                    CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);
                    result = Ok(response);
                }
                else
                {
                    CreatedCustomer response = mapper.Map<CreatedCustomer>(customerBo);
                    result = BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpGet("GetMostLoyalCustomers")]
        public async Task<IActionResult> GetMostLoyalCustomersAsync()
        {
            ObjectResult result;

            try
            {
                List<Customer> products = await service.MostLoyalCustomersAsync(repository.QueryCustomers(), 10);
                List<CreatedCustomer> response = mapper.Map<List<CreatedCustomer>>(products);

                result = Ok(response);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }
    }
}