using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
//using api.Helpers;
//using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks
                .ToList()
                .Select(s => s.ToStockDto());

            return new OkObjectResult(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id).ToStockDto();

            if (stock is null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(stock);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromRequestDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            var result = new CreatedAtActionResult(nameof(GetByID), nameof(StockController), new { id = stockModel.Id }, stockModel.ToStockDto());
            return new CreatedAtActionResult(nameof(GetByID), "Stock", new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel is null)
                return new NotFoundResult();

            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            _context.SaveChanges();

            return new OkObjectResult(stockModel.ToStockDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel is null)
                return new NotFoundResult();

            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
