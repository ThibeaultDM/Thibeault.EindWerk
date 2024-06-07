using Microsoft.AspNetCore.Mvc;

namespace Thibeault.EindWerk.Api.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
