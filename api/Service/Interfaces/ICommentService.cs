using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Service.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllComments();
        Task<CommentDto?> GetCommentById(int id);
        Task DeleteComment(int id);
        Task<CommentDto> AddComment(int id, CreateCommentRequest request);
        Task<CommentDto> UpdateComment(int id, UpdateCommentRequest request);
    }
}