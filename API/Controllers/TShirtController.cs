using AutoMapper;
using Domain.Entities;
using Domain.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Services.DTOs.GenderDTOs;
using Services.DTOs.TshirtDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TshirtController : ControllerBase
    {
        private readonly ITshirtService _tshirtService;
        private readonly IGenderService _genderService;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public TshirtController(ITshirtService tshirtService, IGenderService genderService, ILoggerService logger, IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _logger = logger;
            _tshirtService = tshirtService;
            _genderService = genderService;
            _userManager = userManager;
        }

        private void SetResponseHeaders(MetaData metaData)
        {
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTShirts([FromQuery] TshirtParameters tshirtParameters)
        {
            var tshirtsWithMetadata =  await _tshirtService.GetTShirtsAsync(tshirtParameters);
            SetResponseHeaders(tshirtsWithMetadata.MetaData);
            var tshirtsPage = _mapper.Map<IEnumerable<TShirtToReturnDTO>>(tshirtsWithMetadata);
            
            return Ok(tshirtsPage);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tshirtService.GetByIdAsync(id);
            
            if (!result.Success)
            {
                _logger.LogError($"T-Shirt with id: {id} not found.");

                return BadRequest(result);
            }

            _logger.LogInfo($"Received a t-shirt with id: {id}.");

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("getByUser")]
        public async Task<IActionResult> GetByCurrentUser([FromQuery] TshirtParameters tshirtParameters)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            tshirtParameters.Author = email;
            var tshirtsWithMetadata = await _tshirtService.GetTShirtsAsync(tshirtParameters);
            SetResponseHeaders(tshirtsWithMetadata.MetaData);
            var tshirts = _mapper.Map<IEnumerable<TShirtToReturnDTO>>(tshirtsWithMetadata);
            _logger.LogInfo($"Received a t-shirt for user with email: {email}.");

            return Ok(tshirts);
        }

        [HttpGet]
        [Route("getByAuthor/{name}")]
        public async Task<IActionResult> GetByAuthorName(string name, [FromQuery] TshirtParameters tshirtParameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError("User name send from client is null or empty.");

                return BadRequest("User name is null or empty.");
            }

            var author = _userManager.Users.FirstOrDefault(c => c.DisplayName.Equals(name));
           
            if (author is null)
            {
                _logger.LogError($"User with name {name} not found.");
                
                return BadRequest($"User with name {name} not found.");
            }
            
            tshirtParameters.Author = author.Email; 
            var tshirtsWithMetadata = await _tshirtService.GetTShirtsAsync(tshirtParameters);
            SetResponseHeaders(tshirtsWithMetadata.MetaData);
            var tshirts =  _mapper.Map<IEnumerable<TShirtToReturnDTO>>(tshirtsWithMetadata);
            _logger.LogInfo($"T-shirts received from the author with the name: {name}.");

            return Ok(tshirts);
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateTshirtDTO model)
        {
            if (model is null)
            {
                _logger.LogError("CreateTshirtDTO object send from client is null");
                
                return BadRequest("CreateTshirtDTO object is null");
            }

            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var tshirt = await _tshirtService.CreateAsync(model, email);
            _logger.LogInfo("Create a new t-shirt.");

            return Ok(tshirt);
        }

        [HttpGet]
        [Route("gender")]
        public async Task<IActionResult> GetListOfGenders()
        {
            var result = _mapper.Map<IEnumerable<GenderDTO>>(await _genderService.GetAllGendersAsync());
            _logger.LogInfo("Get list of genders");
            
            return Ok(result);
        }
    }
}
