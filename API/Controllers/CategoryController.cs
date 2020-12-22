using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.CategoryDTOs;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService service, ILoggerService logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        { 
            var categories = await _service.GetAllCategoriesAsync(false);
            var result = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
            _logger.LogInfo("Get all categories.");
            
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryForCreateDTO category)
        {
            if (category is null)
            {
                _logger.LogError($"{nameof(Category)} is null.");

                return BadRequest($"{nameof(Category)} is null.");
            }
            
            await _service.CreateAsync(_mapper.Map<Category>(category));
            _logger.LogInfo($"Category {category.Name} has created.");
            
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync(Category category)
        {
            if (category is null)
            {
                _logger.LogError($"{nameof(Category)} is null.");

                return BadRequest($"{nameof(Category)} is null.");
            }

            var categoryForDelete = await _service.GetByIdAsync(category.Id, false);

            if (categoryForDelete is null)
            {
                _logger.LogError($"No such category with id {category.Id}.");

                return BadRequest($"Category with id {category.Id} not found.");
            }
            
            await _service.DeleteAsync(categoryForDelete);
            _logger.LogInfo($"Category {category.Name} has deleted.");

            return Ok();
        }
    }
}