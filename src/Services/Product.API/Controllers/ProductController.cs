using Microsoft.AspNetCore.Mvc;
using Product.API.Models;
using Product.API.Services.Interfaces;
using Shared.DTOs.Product;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int page)
        {
            if (page < 1)
            {
                page = 1;
            }

            var products = await _productService.GetProducts(page);
            return Ok(products);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute]long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var id = await _productService.CreateProduct(dto);
            return StatusCode(StatusCodes.Status201Created, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] ProductUpdateDto dto)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            await _productService.UpdateProduct(id, dto);
            return Ok(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            await _productService.DeleteProduct(id);
            return Ok(StatusCodes.Status204NoContent);
        }
    }
}
