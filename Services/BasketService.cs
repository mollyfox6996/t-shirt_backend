using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }

        public async Task<BasketDTO> GetBasketAsync(string id)
        {
            var basketResult = await _basketRepository.GetBasketAsync(id);
            var basket = basketResult ?? await _basketRepository.CreateBasketAsync(id);
            return _mapper.Map<CustomBasket, BasketDTO>(basket);
        }

        public async Task<BasketDTO> UpdateBasketAsync(BasketDTO basket)
        {
            var basketToUpdate = _mapper.Map<BasketDTO, CustomBasket>(basket);
            var basketResult = await _basketRepository.UpdateBasketAsync(basketToUpdate);
            return _mapper.Map<CustomBasket, BasketDTO>(basketResult);

        }
    }
}
