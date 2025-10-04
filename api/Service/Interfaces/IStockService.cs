using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;

namespace api.Service.Interfaces
{
    public interface IStockService
    {
        Task<List<StockDto>> GetAllStocks();
        Task<StockDto?> GetStockById(int id);
        Task<StockDto> AddStock(CreateStockRequest stockDto);
        Task DeleteStock(int id);
        Task<StockDto> UpdateStock(int id, UpdateStockRequest stockDto);

    }
}