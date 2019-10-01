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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _customersService.GetAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            customer = await _customersService.AddAsync(customer);
            return CreatedAtAction(nameof(Get), new {id = customer.Id}, customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if(customer.Id != id)
                return BadRequest();

            if(await _customersService.GetAsync(id) == null)
                return NotFound();

            if(!await _customersService.UpdateAsync(customer))
                return StatusCode(304);
            
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customersService.GetAsync(id);
            if(customer == null)
                return NotFound();
            
            if(!await _customersService.DeleteAsync(id))
                return StatusCode(304);
            return NoContent();
        }
    }
}