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
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context) => _context = context;

        public async Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentAsync(int id) => await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

        public async Task SaveAsync(Comment request)
        {
            var comment = await _context.Comments
                                        .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (comment == null)
            {
                await _context.Comments.AddAsync(request);
            }
            else
            {
                _context.Comments.Update(comment);
            }

            await _context.SaveChangesAsync();
        }

    }
}