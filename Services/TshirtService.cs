using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class TshirtService : ITshirtService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public TshirtService(IRepositoryManager repositoryManager, UserManager<AppUser> userManager, IMapper mapper, ILoggerService loggerService)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<PagedList<TShirt>> GetTShirtsAsync(TShirtParameters tshirtParameters) =>
            await _repositoryManager.TShirt
                .GetTShirtListAsync(tshirtParameters, false);
      
        public async Task<PagedList<TShirt>> GetAllByCurrentUserAsync(string email, TShirtParameters tshirtParameters)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return await _repositoryManager.TShirt.GetTshirtsByUserAsync(user.Id, tshirtParameters, false);
        }

        public async Task<PagedList<TShirt>> GetByUserAsync(string id, TShirtParameters tshirtParameters) =>
            await _repositoryManager.TShirt.
                GetTshirtsByUserAsync(id, tshirtParameters, false);
        

        public async Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id) 
        {
            var result = await _repositoryManager.TShirt.GetTShirtByIdAsync(id, false);
            
            if (result == null)
            {
                return new OperationResultDTO<TShirtToReturnDTO>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = null
                };
            }
            return new OperationResultDTO<TShirtToReturnDTO>
            {
                Success = true,
                Message = null,
                Data = _mapper.Map<TShirt, TShirtToReturnDTO>(result)
            };  
        }

        public async Task<OperationResultDTO<string>> CreateAsync(CreateTshirtDTO model, string email)
        {
            OperationResultDTO<string> result = new OperationResultDTO<string>();
            AppUser user = await _userManager.FindByEmailAsync(email);
            Category category = await _repositoryManager.Category.FindCategoryAsync(c => c.Name == model.Category, false);
            Gender gender = await _repositoryManager.Gender.FindGenderAsync(c => c.Name == model.Gender, false);
            TShirt shirt = new TShirt
            {
                Name = model.Name,
                Description = model.Description,
                PictureUrl = model.PictureUrl,
                Price = model.Price,
                UserId = user.Id,
                CategoryId = category.Id,
                GenderId = gender.Id,
                CreateDate = DateTime.Now.ToString("d")
            };

            try
            {
                _repositoryManager.TShirt.CreateTShirt(shirt);
                await _repositoryManager.SaveAsync();
                result.Success = true;
                result.Message = "New tshirt has been created";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Is there something wrong";
                result.Data = ex.Message;
                return result;
            }
        }
    }
}
