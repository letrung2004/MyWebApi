using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.Implementations
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context) => _context = context;
        public async Task Async(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Stock stock)
        {
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Stock>> GetAllAsync() => await _context.Stocks.Include(c => c.Comments).ToListAsync();

        public async Task<Stock?> GetByIdAsync(int id) => await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);

        // public async Task SaveAsync(Stock stock)
        // {
        //     await _context.Stocks.AddAsync(stock);
        //     await _context.SaveChangesAsync();
        // }
        public async Task SaveAsync(Stock request)
        {
            var stock = await _context.Stocks
                                        .FirstOrDefaultAsync(s => s.Id == request.Id);

            if (stock == null)
            {
                await _context.Stocks.AddAsync(request);
            }
            else
            {
                _context.Stocks.Update(stock);
            }

            await _context.SaveChangesAsync();
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}