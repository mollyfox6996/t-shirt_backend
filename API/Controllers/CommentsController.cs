using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
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
        public async Task<IActionResult> AddComment(CreateCommentDTO createCommentDTO)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            await _commentService.AddComent(createCommentDTO, email);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IEnumerable<CommentDTO>> GetCommentsByTshirtIdAsync(int id) => await _commentService.GetTshirtComments(id);
    }
}
