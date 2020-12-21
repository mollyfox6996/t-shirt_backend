using AutoMapper;
using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
using Services.DTOs.OrderAggregate;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Services.DTOs;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IEmailService _emailService;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepositoryManager repositoryManager, IEmailService emailService, IBasketRepository basketRepository, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _emailService = emailService;
            _mapper = mapper;
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
            }

            _repositoryManager.Order.CreateOrder(order);
            await _repositoryManager.SaveAsync();

            var text = $"<h1>Dear {order.Address.FirstName} {order.Address.LastName}, your order.</h1>" +
                       $"<p>Delivery address: {order.Address.City}, {order.Address.Street}, {order.Address.ZipCode}</p>" +
                       $"<p>Delivery method: {order.DeliveryMethod.Name}</p>" +
                       $"Total bill: ${order.Total}";

            await _emailService.SendOrderEmail(order.Email, "Order", text);
            
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