using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Services.DTOs.BasketDTOs;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly ILoggerService _loggerService;

        public BasketController(IBasketService basketService, ILoggerService loggerService)
        {
            _basketService = basketService;
            _loggerService = loggerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasketAsync(string id)
        {
            var basket = await _basketService.GetBasketAsync(id);
            _loggerService.LogInfo($"Get basket for user with id: {id}");

            return Ok(basket);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasketAsync(BasketDTO basket)
        {
            if (basket is null)
            {
                _loggerService.LogError("BasketDTO object send from client is null.");
                return BadRequest();
            }

            var result = await _basketService.UpdateBasketAsync(basket);
            _loggerService.LogInfo("Basket has successful updated.");

            return Ok(result);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketService.DeleteBasketAsync(id);
            _loggerService.LogInfo($"Basket with id: {id} has deleted.");
        }
    }
}
