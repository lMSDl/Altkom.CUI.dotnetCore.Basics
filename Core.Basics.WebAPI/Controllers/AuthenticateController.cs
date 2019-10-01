using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Basics.IServices;
using Core.Basics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Basics.WebAPI.Controllers
{
    public class AuthenticateController : BaseController
    {
        private IAuthenticateService _authenticateService;
        public AuthenticateController(ILogger<AuthenticateController> logger, IAuthenticateService authenticateService) : base(logger)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] User user) {
            var token = _authenticateService.Authenticate(user.Username, user.Password);

            if(token == null)
            return BadRequest(new {message = "Incorrect credentials"});

            return Ok(token);
        }
    }
}
