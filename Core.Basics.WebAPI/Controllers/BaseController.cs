using Microsoft.AspNetCore.Mvc;

namespace Core.Basics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
