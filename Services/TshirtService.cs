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
      
        public TshirtService(IRepositoryManager repositoryManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PagedList<TShirt>> GetTShirtsAsync(TshirtParameters tshirtParameters) =>
            await _repositoryManager.Tshirt
                .GetTShirtListAsync(tshirtParameters, false);
        
        public async Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id) 
        {
            var result = await _repositoryManager.Tshirt.GetTShirtByIdAsync(id, false);
            
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
            var result = new OperationResultDTO<string>();
            var user = await _userManager.FindByEmailAsync(email);
            var category = await _repositoryManager.Category.FindCategoryAsync(c => c.Name == model.Category, false);
            var gender = await _repositoryManager.Gender.FindGenderAsync(c => c.Name == model.Gender, false);
            
            try
            {
                var shirt = new TShirt
                {
                    Name = model.Name,
                    Description = model.Description,
                    PictureUrl = model.PictureUrl,
                    Price = model.Price,
                    UserId = user.Id,
                    CategoryId = category.Id,
                    GenderId = gender.Id,
                    CreateDate = DateTime.Now
                };
           
                _repositoryManager.Tshirt.CreateTShirt(shirt);
                await _repositoryManager.SaveAsync();
                result.Success = true;
                result.Message = "New t-shirt has been created";
                
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "There is something wrong";
                result.Data = ex.Message;
                return result;
            }
        }
    }
}
