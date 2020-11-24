﻿using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TshirtService : ITshirtService
    {
        private readonly ITshirtRepository _tshirtRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public TshirtService(ITshirtRepository tshirtRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _tshirtRepository = tshirtRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<OperationResultDTO<string>> CreateAsync(CreateTshirtDTO model, string email)
        {
            OperationResultDTO<string> result = new OperationResultDTO<string>();
            AppUser user = await _userManager.FindByEmailAsync(email);
            Category category = await _tshirtRepository.GetCategoryAsync(model.Category);
            TShirt shirt = new TShirt
            {
                Name = model.Name,
                Description = model.Description,
                PictureUrl = model.PictureUrl,
                Price = model.Price,
                User = user,
                Category = category,
                CreateDate = DateTime.Now.ToString("d")
            };

            try
            {
                await _tshirtRepository.CreateTShirtAsync(shirt);
                result.Success = true;
                result.Message = "New tshirt has been created";
                return result;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "Is there something wrong";
                result.Data = ex.Message;
                return result;
            }
            
        }

        public async Task<IEnumerable<TShirtToReturnDTO>> GetAllAsync()
        {
            var result = await _tshirtRepository.GetTShirtListAsync();
            return _mapper.Map<IEnumerable<TShirt>, IEnumerable<TShirtToReturnDTO>>(result);
        }

        public async Task<IEnumerable<TShirtToReturnDTO>> GetAllByCurrentUserAsync(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            var result = await _tshirtRepository.GetTshirtByCurrentUserAsync(user.Id);
            return _mapper.Map<IEnumerable<TShirt>, IEnumerable<TShirtToReturnDTO>>(result);
        }

        public async Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id) 
        {
            var result = await _tshirtRepository.GetTShirtByIdAsync(id);
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

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var result = await _tshirtRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(result);
        }

        public async Task<IEnumerable<TShirtToReturnDTO>> GetByUserAsync(string name)
        {
            var result = await _tshirtRepository.GetByAuthorAsync(name);
            return _mapper.Map<IEnumerable<TShirt>, IEnumerable<TShirtToReturnDTO>>(result);
        }
    }
}