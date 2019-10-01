using System.Threading.Tasks;
using Core.Basics.IServices;
using Core.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Basics.WebAPI.Controllers
{
    public class ProductsController : BaseController
    {
        private IProductsService _productsService;
        public ProductsController(ILogger<ProductsController> logger, IProductsService productsService) : base(logger)
        {
            _productsService = productsService;
        }

[HttpGet]
        public async Task<IActionResult> Get() {
            return Ok(await _productsService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productsService.GetAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            product = await _productsService.AddAsync(product);
            return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if(product.Id != id)
                return BadRequest();

            if(await _productsService.GetAsync(id) == null)
                return NotFound();

            if(!await _productsService.UpdateAsync(product))
                return StatusCode(304);
            
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productsService.GetAsync(id);
            if(product == null)
                return NotFound();
            
            if(!await _productsService.DeleteAsync(id))
                return StatusCode(304);
            return NoContent();
        }
    }
}