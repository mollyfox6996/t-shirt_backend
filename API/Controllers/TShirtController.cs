using AutoMapper;
using Domain.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.DTOs;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TShirtController : ControllerBase
    {
        private readonly ITshirtService _tshirtService;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public TShirtController(ITshirtService tshirtService, ILoggerService logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
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
        public async Task<IEnumerable<TShirtToReturnDTO>> GetTShirts([FromQuery] TShirtParameters tshirtParameters)
        {
            var tshirtsWithMetadata =  await _tshirtService.GetTShirtsAsync(tshirtParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(tshirtsWithMetadata.MetaData));
            
            return _mapper.Map<IEnumerable<TShirtToReturnDTO>>(tshirtsWithMetadata);
        }

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
