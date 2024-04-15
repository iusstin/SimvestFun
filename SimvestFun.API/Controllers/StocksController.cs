using Microsoft.AspNetCore.Mvc;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : BaseController
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService, IJwtUtils jwtUtils) : base(jwtUtils)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult<Stock>> GetAllStocks()
        {
            var result = await _stockService.GetAllStocksAsync();
            return Ok(result);
        }

        [HttpGet("prices/{id}")]
        public async Task<ActionResult<Stock>> GetStockWithPrices(string id)
        {
            var result = await _stockService.GetStockWithPricesAsync(id);
            return Ok(result);
        }


    }
}
