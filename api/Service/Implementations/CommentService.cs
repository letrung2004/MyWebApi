using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Mappers;
using api.Repository.Interfaces;
using api.Service.Interfaces;

namespace api.Service.Implementations
{

    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockService _stockService;
        public CommentService(ICommentRepository commentRepo, IStockService stockService)
        {
            _commentRepo = commentRepo;
            _stockService = stockService;
        }

        public async Task<CommentDto> AddComment(int id, CreateCommentRequest request)
        {
            if (!await _stockService.StockExists(id))
            {
                throw new KeyNotFoundException($"Stock with id {id} not found.");
            }
            var commentModel = request.toCommentFromCreate(id);
            await _commentRepo.SaveAsync(commentModel);
            return commentModel.toCommentDto();
        }

        public async Task DeleteComment(int id)
        {
            var commentModel = await _commentRepo.GetCommentAsync(id);
            if (commentModel == null)
            {
                throw new KeyNotFoundException($"Comment with id {id} not found.");
            }
            await _commentRepo.DeleteAsync(commentModel);
        }

        public async Task<List<CommentDto>> GetAllComments()
        {
            var comments = await _commentRepo.GetAllAsync();
            return comments.Select(c => c.toCommentDto()).ToList();
        }

        public async Task<CommentDto?> GetCommentById(int id)
        {
            return (await _commentRepo.GetCommentAsync(id))?.toCommentDto();
        }

        public async Task<CommentDto> UpdateComment(int id, UpdateCommentRequest request)
        {
            var commentModel = await _commentRepo.GetCommentAsync(id);
            if (commentModel == null)
            {
                throw new KeyNotFoundException($"Comment with id {id} not found.");
            }
            commentModel.toCommentFromUpdate(request);
            await _commentRepo.SaveAsync(commentModel);
            return commentModel.toCommentDto();
        }
    }
}