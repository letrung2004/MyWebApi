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
    }
}