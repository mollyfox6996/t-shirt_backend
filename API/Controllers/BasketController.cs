using Microsoft.AspNetCore.Authorization;
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
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [Authorize]
        [HttpGet]
        public async Task<BasketDTO> GetBasketAsync(string id) => await _basketService.GetBasketAsync(id);

        [Authorize]
        [HttpPatch]
        public async Task<BasketDTO> UpdateBasketAsync(BasketDTO basket) => await _basketService.UpdateBasketAsync(basket);

        [Authorize]
        [HttpDelete]
        public async Task DeleteBasketAsync(string id) => await _basketService.DeleteBasketAsync(id);
    }
}
