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

            return _mapper.Map<CustomBasket, BasketDTO>(basket);
        }

        public async Task<BasketDTO> UpdateBasketAsync(BasketDTO basket)
        {
            var customBasket = await _repositoryManager.Basket.GetBasketAsync(basket.Id, true);
            _mapper.Map(basket, customBasket);
            await _repositoryManager.SaveAsync();

            return basket;
        }

        public async Task DeleteBasketAsync(BasketDTO basket)
        {
            _repositoryManager.Basket.DeleteBasket(_mapper.Map<BasketDTO, CustomBasket>(basket));
            await _repositoryManager.SaveAsync();
        }
    }
}
