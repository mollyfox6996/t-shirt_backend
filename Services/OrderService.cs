using AutoMapper;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;
using Services.DTOs.OrderAggregate;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public OrderService(IRepositoryManager repositoryManager, IBasketRepository basketRepository, UserManager<AppUser> userManager, IMapper mapper, ILoggerService loggerService)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _mapper = mapper;
            _loggerService = loggerService;
            _basketRepository = basketRepository;
        }
        public async Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO, string email)
        {
            var address = _mapper.Map<Address>(orderDTO.Address);
            var basket = await _basketRepository.GetBasketAsync(orderDTO.BasketId);
            var items = new List<OrderItem>();

            foreach(var item in basket.Items)
            {
                var shirtItem = await _repositoryManager.TShirt.GetTShirtByIdAsync(item.Id, true);
                var orderItem = new OrderItem
                {
                    Name = shirtItem.Name,
                    Price = shirtItem.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
            }

            var deliveryMethod = await _repositoryManager.DeliveryMethod.GetDeliveryMethodAsync(orderDTO.DeliveryMethodId);
            var total = items.Sum(i => i.Price * i.Quantity) + deliveryMethod.Price;
            
            var order = new Order
            {
                Email = email,
                OrderDate = DateTime.Now,
                Address = address,
                DeliveryMethod = deliveryMethod,
                OrderItems = items,
                Total = total,

            };

            var existOrder = await _repositoryManager.Order.GetOrderByIdAsync(order.Id, order.Email);

            if(existOrder != null)
            {
                _repositoryManager.Order.DeleteOrder(existOrder);
                _repositoryManager.Order.CreateOrder(order);
            }

            _repositoryManager.Order.CreateOrder(order);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<OrderToReturnDTO>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethods()  
        {
            var result = await _repositoryManager.DeliveryMethod.GetDeliveryMethodsAsync(true);
            return _mapper.Map<IEnumerable<DeliveryMethodDTO>>(result);
        }

        public async Task<OrderToReturnDTO> GetOrderAsync(int id, string email)
        {
            var result = await _repositoryManager.Order.GetOrderByIdAsync(id, email);
            return _mapper.Map<OrderToReturnDTO>(result);
        }

        public async Task<IEnumerable<OrderToReturnDTO>> GetOrdersForUserAsync(string email)
        {
            var result = await _repositoryManager.Order.GetOrdersForUserAsync(email);
            return _mapper.Map<IEnumerable<OrderToReturnDTO>>(result);
        }
    }
}