using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Basics.WebAPI.Controllers
{
    public class LoopController : BaseController
    {
        public LoopController(ILogger<LoopController> logger) : base(logger)
        {
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var orderLoop = new ProductLoop{Name = "Loop"};
            orderLoop.Product = orderLoop;

            return Ok(orderLoop);
        }
        
        private class ProductLoop : Product {
                public Product Product {get; set;}
        }
    }
}
