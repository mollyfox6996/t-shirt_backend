using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Services.DTOs;
using Services.Interfaces;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<BasketDTO> GetBasketAsync(string id)
        {
            var basketResult = await _basketRepository.GetBasketAsync(id);
            var basket = basketResult ?? new CustomBasket(id);

            return _mapper.Map<CustomBasket, BasketDTO>(basket);
        }

        public async Task<BasketDTO> UpdateBasketAsync(BasketDTO basket)
        {
            
            var customBasket = await _basketRepository.UpdateBasketAsync(_mapper.Map<BasketDTO, CustomBasket>(basket));
            _mapper.Map(basket, customBasket);

            return basket;
        }

        public async Task DeleteBasketAsync(BasketDTO basket)
        {
            await _basketRepository.DeleteBasketAsync(basket.Id);
            
        }
    }
}
