using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
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
        public async Task<IActionResult> AddRating(AddRatingDTO addRatingDTO)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            await _ratingService.AddRating(addRatingDTO, email);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRatingByShirtId(int id) => Ok(await _ratingService.GetRatingByShirt(id));

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckRatingExist([FromBody] int shirtId)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return Ok(await _ratingService.CheckRatingFromUser(shirtId, email));
        }
    }
}
