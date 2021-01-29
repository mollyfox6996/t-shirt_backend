using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Services.DTOs.RatingDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : BaseController
    {
        private readonly IRatingService _ratingService;
        private readonly ILoggerService _logger;

        public RatingController(IRatingService ratingService, ILoggerService logger)
        {
            _ratingService = ratingService;
            _logger = logger;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddRating(AddRatingDTO addRatingDto)
        {
            var email = GetEmailFromHttpContextAsync();
            await _ratingService.AddRating(addRatingDto, email);
            _logger.LogInfo($"Add rating by user with email: {email}.");
            
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRatingByShirtId(int id)
        {
            var result = await _ratingService.GetRatingByShirt(id);

            if (result is null)
            {
                _logger.LogError($"Not found t-shirt with id: {id}");

                return BadRequest($"Not found t-shirt with id: {id}");
            }
            
            _logger.LogInfo($"Get rating for t-shirt with id: {id}");

            return Ok(result);
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckRatingExist([FromBody] int shirtId)
        {
            var email = GetEmailFromHttpContextAsync();
            var result = await _ratingService.CheckRatingFromUser(shirtId, email);
            _logger.LogInfo($"Check rating for t-shirt with id: {shirtId}");

            return Ok(result);
        }
    }
}
