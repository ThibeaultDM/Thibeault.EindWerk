using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.DataLayer;
using Thibeault.EindWerk.DataLayer.Interfaces;

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
        public Task<IActionResult> CreateCustomer([FromBody] CreateCustomer)
        {
            
        }
    }
}
