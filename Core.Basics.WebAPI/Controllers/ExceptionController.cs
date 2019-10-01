using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Basics.WebAPI.Controllers
{
    public class ExceptionController : BaseController
    {
        public ExceptionController(ILogger<ExceptionController> logger) : base(logger)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new Exception("Something wrong");
        }
    }
}
