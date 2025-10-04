using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Comment;
using api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IStockService _stockService;


        public CommentController(ICommentService commentService, IStockService stockService)
        {
            _commentService = commentService;
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentService.GetAllComments();
            var response = new ApiResponse<List<CommentDto>>(1000, comments);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentService.GetCommentById(id);
            if (comment == null) return NotFound();
            return Ok(new ApiResponse<CommentDto>(1000, comment));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            try
            {
                await _commentService.DeleteComment(id);
                return Ok(new ApiResponse<string>(1000, "Comment deleted successfully"));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentRequest request)
        {
            if (!await _stockService.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var createdComment = await _commentService.AddComment(stockId, request);
            var response = new ApiResponse<CommentDto>(1000, createdComment);
            return CreatedAtAction(nameof(GetById), new { id = createdComment.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequest request)
        {
            var updateComment = await _commentService.UpdateComment(id, request);
            var response = new ApiResponse<CommentDto>(1000, updateComment);
            return Ok(response);

        }


    }
}