using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetCommentAsync(int id);
        Task SaveAsync(Comment comment);
        Task DeleteAsync(Comment comment);
    }
}