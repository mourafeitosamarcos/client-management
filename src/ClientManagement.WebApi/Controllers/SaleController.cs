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
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetAll()
        {
            var sales = await _saleService.GetAll();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDTO>> GetById(int id)
        {
            var sale = await _saleService.GetById(id);
            if (sale == null)
                return NotFound();
            return Ok(sale);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetByClientId(int clientId)
        {
            var sales = await _saleService.GetByClientId(clientId);
            return Ok(sales);
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetByProductId(int productId)
        {
            var sales = await _saleService.GetByProductId(productId);
            return Ok(sales);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SaleDTO sale)
        {
            if (sale == null)
                return BadRequest();

            var createdSale = await _saleService.Add(sale);
            return CreatedAtAction(nameof(GetById), new { id = createdSale.Id }, createdSale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaleDTO sale)
        {
            if (sale == null || sale.Id != id)
                return BadRequest();

            var updatedSale = await _saleService.Update(sale);
            if (updatedSale == null)
                return NotFound();

            return Ok(updatedSale);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _saleService.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
