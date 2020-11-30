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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<BasketDTO> GetBasketAsync(string id) => await _basketService.GetBasketAsync(id);

        [HttpPut]
        public async Task<BasketDTO> UpdateBasketAsync(BasketDTO basket) => await _basketService.UpdateBasketAsync(basket);

        [HttpDelete]
        public async Task DeleteBasketAsync(string id) => await _basketService.DeleteBasketAsync(id);
    }
}
