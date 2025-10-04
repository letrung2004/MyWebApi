using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto toCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Content = comment.Content,
                Title = comment.Title,
                CreateOn = comment.CreateOn,
                StockId = comment.StockId,
            };
        }
    }
}