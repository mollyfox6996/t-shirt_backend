using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Services.DTOs.OperationResultDTOs;
using Services.DTOs.TshirtDTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class TshirtService : ITshirtService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IGenderService _genderService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
      
        public TshirtService(IRepositoryManager repositoryManager, IGenderService genderService, ICategoryService categoryService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _genderService = genderService;
            _categoryService = categoryService;
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
            var category = await _categoryService.FindCategoryAsync(c => c.Name == model.Category, false);
            var gender = await _genderService.FindGenderAsync(c => c.Name == model.Gender, false);
            
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
