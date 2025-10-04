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
        public CommentService(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }
        public async Task<List<CommentDto>> GetAllComments()
        {
            var comments = await _commentRepo.GetAllAsync();
            return comments.Select(c => c.toCommentDto()).ToList();
        }
    }
}