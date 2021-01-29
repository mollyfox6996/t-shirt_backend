using AutoMapper;
using Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.OrderAggregate;
using Services.Interfaces;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, ILoggerService logger, IMapper mapper)
        {
            _orderService = orderService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            if(result is null)
            {
                _logger.LogError("Error! Orders not found.");
                
                return NoContent();
            }
            
            _logger.LogInfo("Get all orders.");
            
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO order)
        {
            var email = GetEmailFromHttpContextAsync();
            var result = await _orderService.CreateOrderAsync(order, email);
            _logger.LogInfo($"Create order for user with email: {email}.");
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var email = GetEmailFromHttpContextAsync();
            var result = await _orderService.GetOrdersForUserAsync(email);
            _logger.LogInfo($"Get order for user with email: {email}.");
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderForUserById(int id)
        {
            var email = GetEmailFromHttpContextAsync();
            var result = await _orderService.GetOrderAsync(id, email);
            _logger.LogInfo($"Get order for user with email: {email}, by id: {id}.");
            return Ok(result);
        }

        [HttpGet]
        [Route("deliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var result = await _orderService.GetDeliveryMethods();
            
            if(result is null)
            {
                _logger.LogError("Error! Delivery methods not found.");
                
                return NoContent();
            }
            
            _logger.LogInfo("Get delivery methods.");
            
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles="admin")]
        [Route("deliveryMethods")]
        public async Task<IActionResult> DeleteDeliveryMethodAsync(int id)
        {
            await _orderService.DeleteDeliveryMethodAsync(id);
            _logger.LogInfo("Delivery method was deleted");
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles="admin")]
        [Route("deliveryMethods")]
        public async Task<IActionResult> CreateDeliveryMethodAsync(DeliveryMethodDTO method)
        {
            if (method is null)
            {
                _logger.LogError($"{nameof(method)} is null.");

                return BadRequest($"{nameof(method)} is null.");
            }

            await _orderService.CreateDeliveryMethodAsync(_mapper.Map<DeliveryMethod>(method));
            _logger.LogInfo($"Delivery method {method.Name} has created.");
            
            return Ok();
        }
    }
}