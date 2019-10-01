using System.Threading.Tasks;
using Core.Basics.IServices;
using Core.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Basics.WebAPI.Controllers
{
    public class CustomersController : BaseController
    {
        private ICustomersService _customersService;
        public CustomersController(ILogger<CustomersController> logger, ICustomersService customersService) : base(logger)
        {
            _customersService = customersService;
        }

        public async Task<IActionResult> Get() {
            return Ok(await _customersService.GetAsync());
        }
    }
}