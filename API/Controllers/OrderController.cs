using AutoMapper;
using Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.DTOs.OrderAggregate;
using Services.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService _logger;

        public OrderController(IOrderService orderService, ILoggerService logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO order)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var result = await _orderService.CreateOrderAsync(order, email);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var result = await _orderService.GetOrdersForUserAsync(email);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderForUserById(int id)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var result = await _orderService.GetOrderAsync(id, email);
            return Ok(result);
        }

        [HttpGet]
        [Route("deliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var result = await _orderService.GetDeliveryMethods();
            if(result is null)
            {
                return NoContent();
            }
            return Ok(result);
        }
    }
}