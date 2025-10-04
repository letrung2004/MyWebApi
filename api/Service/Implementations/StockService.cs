using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Mappers;
using api.Repository.Interfaces;
using api.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Service.Implementations
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepo;
        public StockService(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<StockDto> AddStock(CreateStockRequest stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            await _stockRepo.SaveAsync(stockModel);
            return stockModel.toStockDto();
        }

        public async Task DeleteStock(int id)
        {
            var stockModel = await _stockRepo.GetByIdAsync(id);
            if (stockModel == null)
            {
                throw new KeyNotFoundException($"Stock with id {id} not found.");
            }
            await _stockRepo.DeleteAsync(stockModel);
        }

        public async Task<List<StockDto>> GetAllStocks()
        {
            var stocks = await _stockRepo.GetAllAsync();
            return stocks.Select(s => s.toStockDto()).ToList();
        }

        public async Task<StockDto?> GetStockById(int id)
        {
            return (await _stockRepo.GetByIdAsync(id))?.toStockDto();
        }

        public async Task<StockDto> UpdateStock(int id, UpdateStockRequest stockDto)
        {
            var stockModel = await _stockRepo.GetByIdAsync(id);
            if (stockModel == null)
            {
                throw new KeyNotFoundException($"Stock with id {id} not found.");
            }
            stockModel.UpdateStockFromDto(stockDto);
            await _stockRepo.SaveAsync(stockModel);
            return stockModel.toStockDto();
        }
    }
}