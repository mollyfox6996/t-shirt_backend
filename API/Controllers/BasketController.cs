<<<<<<< HEAD
﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;
=======
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
>>>>>>> refs/remotes/origin/dev
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
<<<<<<< HEAD
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, ILoggerService loggerService, IMapper mapper)
        {
            _basketService = basketService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasketAsync(string id)
        {
            var basket = await _basketService.GetBasketAsync(id, false);
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
            _loggerService.LogInfo("Basket has succesfully updated.");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
            var basket = await _basketService.GetBasketAsync(id, false);
            await _basketService.DeleteBasketAsync(basket);
            _loggerService.LogInfo($"Basket with id: {id} has deleted.");

            return NoContent();
        }
=======

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
>>>>>>> refs/remotes/origin/dev
    }
}
