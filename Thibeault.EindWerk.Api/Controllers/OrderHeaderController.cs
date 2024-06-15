using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Services;

namespace Thibeault.EindWerk.Api.Controllers
{
    public class OrderHeaderController : Controller
    {
        private readonly IOrderHeaderRepository orderRepository;
        private readonly OrderHeaderService orderService;
        private readonly IProductRepository productRepository;
        private readonly IStockActionRepository actionRepository;
        private readonly IMapper mapper;

        public OrderHeaderController(IOrderHeaderRepository orderRepository, OrderHeaderService orderService, IProductRepository productRepository, IStockActionRepository actionRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.orderService = orderService;
            this.productRepository = productRepository;
            this.actionRepository = actionRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
