using Auth.Core.DTOs;
using Auth.Core.Entities;
using Auth.Core.Services;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProudcts([FromQuery] string? name,
    [FromQuery] int? categoryId)
        {
            var result = await _service.GetProudctsAsync(name, categoryId);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest req)
        {

            var result = await _service.CreateAsync(req);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
                

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductRequest req)
        {
            var result = await _service.UpdateAsync(id, req);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(result);
            }
               

            return Ok(result);
        }

        [Authorize]
        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData()
        {
            var result = await _service.GetProductFormDataAsync();
            return Ok(result);
        }
    }
}
