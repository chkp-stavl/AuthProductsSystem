using Auth.Api.Common;
using Auth.Core.Common.Auth.Api.Common;
using Auth.Core.DTOs;
using Auth.Core.Services;
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
        public async Task<IActionResult> GetProudcts(
            [FromQuery] string? name,
            [FromQuery] int? categoryId)
        {
            try
            {
                var result =
                    await _service
                        .GetProudctsAsync(name, categoryId);

                if (!result.IsSuccess)
                    return BadRequest(result);

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ErrorMessages.UnexpectedError
                    });
            }
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result =
                    await _service.GetByIdAsync(id);

                if (!result.IsSuccess)
                    return NotFound(result);

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ErrorMessages.UnexpectedError
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProductRequest req)
        {
            try
            {
                var result =
                    await _service.CreateAsync(req);

                if (!result.IsSuccess)
                    return BadRequest(result);

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    500,
                     new
                     {
                         message = ErrorMessages.UnexpectedError
                     });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateProductRequest req)
        {
            try
            {
                var result =
                    await _service.UpdateAsync(id, req);

                if (!result.IsSuccess)
                    return BadRequest(result);

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    500,
                     new
                     {
                         message = ErrorMessages.UnexpectedError
                     });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result =
                    await _service.DeleteAsync(id);

                if (!result.IsSuccess)
                    return NotFound(result);

                return NoContent();
            }
            catch
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ErrorMessages.UnexpectedError
                    });
            }
        }

        [Authorize]
        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData()
        {
            try
            {
                var result =
                    await _service
                        .GetProductFormDataAsync();

                return Ok(result);
            }
            catch
            {
                return StatusCode(
                    500,
                    new
                    {
                        message =ErrorMessages.UnexpectedError
                    });
            }
        }
    }
}
