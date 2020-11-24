using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TShirtController : ControllerBase
    {
        private readonly ITshirtService _tshirtService;

        public TShirtController(ITshirtService tshirtService)
        {
            _tshirtService = tshirtService;
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<OperationResultDTO<string>> Create(CreateTshirtDTO model)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return await _tshirtService.CreateAsync(model, email);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<TShirtToReturnDTO>> GetAll() => await _tshirtService.GetAllAsync();

        [HttpGet]
        [Route("{id}")]
        public async Task<OperationResultDTO<TShirtToReturnDTO>> GetById(int id) => await _tshirtService.GetByIdAsync(id);

        [Authorize]
        [HttpGet]
        [Route("getByUser")]
        public async Task<IEnumerable<TShirtToReturnDTO>> GetByUserId()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return await _tshirtService.GetAllByCurrentUserAsync(email);
        }

        [HttpGet]
        [Route("categories")]
        public async Task<IEnumerable<CategoryDTO>> GetCategories() => await _tshirtService.GetCategoriesAsync();

        [HttpGet]
        [Route("genders")]
        public async Task<IEnumerable<GenderDTO>> GetGenders() => await _tshirtService.GetGendersAsync();

        [HttpGet]
        [Route("getByAuthor/{name}")]
        public async Task<IEnumerable<TShirtToReturnDTO>> GetByAuthorName(string name) => await _tshirtService.GetByUserAsync(name);
    }
}
