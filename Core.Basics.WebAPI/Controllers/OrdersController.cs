using System.Threading.Tasks;
using Core.Basics.IServices;
using Core.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Basics.WebAPI.Controllers
{
    public class OrdersController : BaseController
    {
        private IOrdersService _ordersService;
        public OrdersController(ILogger<OrdersController> logger, IOrdersService ordersService) : base(logger)
        {
            _ordersService = ordersService;
        }

[HttpGet]
        public async Task<IActionResult> Get() {
            return Ok(await _ordersService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _ordersService.GetAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            order = await _ordersService.AddAsync(order);
            return CreatedAtAction(nameof(Get), new {id = order.Id}, order);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Order order)
        {
            if(order.Id != id)
                return BadRequest();

            if(await _ordersService.GetAsync(id) == null)
                return NotFound();

            if(!await _ordersService.UpdateAsync(order))
                return StatusCode(304);
            
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _ordersService.GetAsync(id);
            if(order == null)
                return NotFound();
            
            if(!await _ordersService.DeleteAsync(id))
                return StatusCode(304);
            return NoContent();
        }
    }
}