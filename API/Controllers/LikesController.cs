using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.DTOs.LikeDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : BaseController
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
            var email = GetEmailFromHttpContextAsync();
            await _likeService.AddLike(shirtId, email);
            _logger.LogInfo($"Add like for t-shirt with id: {shirtId}.");
            
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IEnumerable<LikeDTO>> GetLikesByTshirtIdAsync(int id)
        {
            var result = await _likeService.GetLikesByShirt(id);
            _logger.LogInfo($"Get like for t-shirt with id: {id}.");

            return result;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteLike([FromBody] int shirtId)
        {
            var email = GetEmailFromHttpContextAsync();
            await _likeService.DeleteLike(shirtId, email);
            _logger.LogInfo($"Delete like for t-shirt with id: {shirtId}.");
            
            return Ok();
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckLikeExist([FromBody] int shirtId)
        {
            var email = GetEmailFromHttpContextAsync();
            var result = await _likeService.CheckLikeFromUser(shirtId, email);
            _logger.LogInfo($"Check like for t-shirt with id: {shirtId}.");
            
            return Ok(result);
        }
    }
}