﻿using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Services.DTOs.CommentDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly ILoggerService _logger;
        
        public CommentsController(ICommentService commentService, ILoggerService logger)
        {
            _commentService = commentService;
            _logger = logger;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddComment(CreateCommentDTO createCommentDto)
        {
            var email = GetEmailFromHttpContextAsync();
            await _commentService.AddComment(createCommentDto, email);
            _logger.LogInfo($"Add comment for t-shirt with id: {createCommentDto.ShirtId}.");
            
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCommentsByTshirtIdAsync(int id)
        {
            var result = await _commentService.GetTshirtComments(id);
            _logger.LogInfo($"Get comments for t-shirts with id: {id}.");

            return Ok(result);
        }
    }
}
