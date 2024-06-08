using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.DataLayer;
using Thibeault.EindWerk.DataLayer.Interfaces;
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

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer customerToCreate)
        {
            // should an invalid customer have been created it will have a tracking Id of "K0"
            // No need to have it keep taking up space
            Customer customer = await repository.GetCustomerByTrackingNumber("K0");

            if (customer == null)
            {
                customer = await repository.AddCustomer();
            }

            if (customerBo.IsValid)
            {

            }
        }
    }
}
