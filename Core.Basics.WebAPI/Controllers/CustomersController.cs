using Core.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Basics.WebAPI.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(ILogger<CustomersController> logger) : base(logger)
        {
        }

        public IActionResult Get() {
            return Ok(new Customer[] {new Customer{Id = 1, FirstName="Adam", LastName="Adamski"}, new Customer{Id = 1, FirstName="Piotr", LastName="Piotrowski"}});
        }
    }
}