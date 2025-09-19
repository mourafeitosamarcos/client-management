using ClientManagement.Application.Dto;
using ClientManagement.Application.Interfaces.Services;
using ClientManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] ProductDTO product)
        {
            var createdProduct = await _productService.Add(product);
            return CreatedAtAction(nameof(ObterProdutoPorId), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProdutos([FromQuery] decimal? preco_min, [FromQuery] decimal? preco_max)
        {
            var produtos = await _productService.GetByRangePrice(preco_min ?? decimal.MinValue, preco_max ?? decimal.MaxValue);
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> ObterProdutoPorId(int id)
        {
            var produto = await _productService.GetById(id);
            if (produto == null)
                return NotFound();
            return Ok(produto);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> ObterTodosProdutos()
        {
            var produtos = await _productService.GetAll();
            return Ok(produtos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProductDTO product)
        {
            if (id != product.Id)
                return BadRequest();

            var updatedProduct = await _productService.Update(product);
            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var deleted = await _productService.Delete(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
