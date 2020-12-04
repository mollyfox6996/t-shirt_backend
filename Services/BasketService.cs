using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Services.DTOs;
using Services.Interfaces;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Text;
>>>>>>> refs/remotes/origin/dev
using System.Threading.Tasks;

namespace Services
{
    public class BasketService : IBasketService
    {
<<<<<<< HEAD
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public BasketService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<BasketDTO> GetBasketAsync(string id, bool trackChanges)
        {
            var basketResult = await _repositoryManager.Basket.GetBasketAsync(id, trackChanges);
            var basket = basketResult ?? _repositoryManager.Basket.CreateBasket(id);
            await _repositoryManager.SaveAsync();

=======
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
>>>>>>> refs/remotes/origin/dev
            return _mapper.Map<CustomBasket, BasketDTO>(basket);
        }

        public async Task<BasketDTO> UpdateBasketAsync(BasketDTO basket)
        {
<<<<<<< HEAD
            var customBasket = await _repositoryManager.Basket.GetBasketAsync(basket.Id, true);
            _mapper.Map(basket, customBasket);
            await _repositoryManager.SaveAsync();

            return basket;
        }

        public async Task DeleteBasketAsync(BasketDTO basket)
        {
            _repositoryManager.Basket.DeleteBasket(_mapper.Map<BasketDTO, CustomBasket>(basket));
            await _repositoryManager.SaveAsync();
=======
            var basketToUpdate = _mapper.Map<BasketDTO, CustomBasket>(basket);
            var basketResult = await _basketRepository.UpdateBasketAsync(basketToUpdate);
            return _mapper.Map<CustomBasket, BasketDTO>(basketResult);

>>>>>>> refs/remotes/origin/dev
        }
    }
}
