using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.V1.Products.Commands.CreateProduct;
using Product.Application.Features.V1.Products.Commands.DeleteProduct;
using Product.Application.Features.V1.Products.Commands.UpdateProduct;
using Product.Application.Features.V1.Products.Queries.GetProductById;
using Product.Application.Features.V1.Products.Queries.GetProducts;
using Shared.DTOs.Product;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int page)
        {
            if (page < 1)
            {
                page = 1;
            }

            var query = new GetProductsQuery { Page = page };
            var result = await _mediator.Send(query);
            
            if (result.IsSuccessed)
            {
                return Ok(result.Data);
            }
            
            return BadRequest(result.Message);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute]long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var query = new GetProductByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result.IsSuccessed)
            {
                if (result.Data == null)
                {
                    return NotFound();
                }
                return Ok(result.Data);
            }
            
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var command = new CreateProductCommand
            {
                No = dto.No,
                Name = dto.Name,
                Summary = dto.Summary,
                Description = dto.Description,
                Price = dto.Price
            };
            
            var result = await _mediator.Send(command);
            
            if (result.IsSuccessed)
            {
                return StatusCode(StatusCodes.Status201Created, result.Data);
            }
            
            return BadRequest(result.Message);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] ProductUpdateDto dto)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            
            var command = new UpdateProductCommand
            {
                Id = id,
                No = dto.No,
                Name = dto.Name,
                Summary = dto.Summary,
                Description = dto.Description,
                Price = dto.Price
            };
            
            var result = await _mediator.Send(command);
            
            if (result.IsSuccessed)
            {
                return Ok(StatusCodes.Status204NoContent);
            }
            
            return BadRequest(result.Message);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            
            var command = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (result.IsSuccessed)
            {
                return Ok(StatusCodes.Status204NoContent);
            }
            
            return BadRequest(result.Message);
        }
    }
}
