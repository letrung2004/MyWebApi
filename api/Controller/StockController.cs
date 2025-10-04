using System;
using api.Data;
using api.Dtos;
using api.Dtos.Stock;
using api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockService.GetAllStocks();
            var response = new ApiResponse<List<StockDto>>(1000, stocks);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockService.GetStockById(id);
            if (stock == null) return NotFound();
            return Ok(new ApiResponse<StockDto>(1000, stock));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequest request)
        {
            var createdStock = await _stockService.AddStock(request);
            var response = new ApiResponse<StockDto>(1000, createdStock);
            return CreatedAtAction(nameof(GetById), new { id = createdStock.Id }, response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequest request)
        {
            var createdStock = await _stockService.UpdateStock(id, request);
            var response = new ApiResponse<StockDto>(1000, createdStock);
            return Ok(response);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            try
            {
                await _stockService.DeleteStock(id);
                return Ok(new ApiResponse<string>(1000, "Stock deleted successfully"));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}