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
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly ILoggerService _logger;

        public LikesController(ILikeService likeService, ILoggerService logger)
        {
            _likeService = likeService;
            _logger = logger;
        }
        
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddLike([FromBody] int shirtId)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            await _likeService.AddLike(shirtId, email);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IEnumerable<LikeDTO>> GetLikesByTshirtIdAsync(int id) => await _likeService.GetLikesByShirt(id);

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteLike([FromBody] int shirtId)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            await _likeService.DeleteLike(shirtId, email);
            return Ok();
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckLikeExist([FromBody] int shirtId)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return Ok(await _likeService.CheckLikeFromUser(shirtId, email));
        }
    }
}