using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CommetsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILoggerService _logger;

        public CommetsController(ICommentService commentService, ILoggerService logger)
        {
            _commentService = commentService;
            _logger = logger;
        }

        [HttpPost]
        [Route("add")]
        public async Task AddComment(CommentDTO commentDTO) => await _commentService.AddComent(commentDTO);

        [HttpGet]
        [Route("{id}")]
        public async Task<IEnumerable<CommentDTO>> GetCommentsByTshirtIdAsync(int id) => await _commentService.GetTshirtComments(id);
    }
}
