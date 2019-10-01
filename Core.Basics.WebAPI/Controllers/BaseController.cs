using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Basics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ILogger Logger {get;}
        public BaseController(ILogger logger) {
            Logger = logger;
        }
    }
}
