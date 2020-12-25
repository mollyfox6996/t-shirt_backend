using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
using Services.DTOs.OrderAggregate;
using Services.Interfaces;

namespace Services.Services
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

        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync()
        {
            var result = await _repositoryManager.Order.GetOrdersAsync();

            return _mapper.Map<IEnumerable<OrderToReturnDTO>>(result);

        }
        public async Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDto, string email)
        {
            var address = _mapper.Map<Address>(orderDto.Address);
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId);
            var items = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var shirtItem = await _repositoryManager.Tshirt.GetTShirtByIdAsync(item.Id, true);
                var orderItem = new OrderItem
                {
                    Name = shirtItem.Name,
                    Price = shirtItem.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
            }

            var deliveryMethod = await _repositoryManager.DeliveryMethod.GetDeliveryMethodAsync(orderDto.DeliveryMethodId);
            var total = items.Sum(i => i.Price * i.Quantity) + deliveryMethod.Price;
            
            var order = new Order
            {
                Email = email,
                OrderDate = DateTime.Now,
                Address = address,
                DeliveryMethodId = deliveryMethod.Id,
                OrderItems = items,
                Total = total,
            };

            var existOrder = await _repositoryManager.Order.GetOrderByIdAsync(order.Id, order.Email);

            if (existOrder != null)
            {
                _repositoryManager.Order.DeleteOrder(existOrder);
            }

            _repositoryManager.Order.CreateOrder(order);
            await _repositoryManager.SaveAsync();

            var text = $"<h1>Dear {order.Address.FirstName} {order.Address.LastName}, your order.</h1>" +
                       $"<p>Delivery address: {order.Address.City}, {order.Address.Street}, {order.Address.ZipCode}</p>" +
                       $"<p>Delivery method: {deliveryMethod.Name}</p>" +
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

        public async Task CreateDeliveryMethodAsync(DeliveryMethod method)
        {
            _repositoryManager.DeliveryMethod.CreateMethod(method);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteDeliveryMethodAsync(int id)
        {
            var result = await _repositoryManager.DeliveryMethod.GetDeliveryMethodAsync(id);

            _repositoryManager.DeliveryMethod.DeleteMethod(result);
            await _repositoryManager.SaveAsync();
        }
    }
}