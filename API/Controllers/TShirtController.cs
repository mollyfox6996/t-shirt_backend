using AutoMapper;
using Domain.Entities;
using Domain.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public TShirtController(ITshirtService tshirtService, ILoggerService logger, IMapper mapper, UserManager<AppUser> userManager)

        {
            _mapper = mapper;
            _logger = logger;
            _tshirtService = tshirtService;
            _userManager = userManager;
        }

        private void SetResponseHeaders(MetaData metaData)
        {
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
        }


        [HttpGet]
        public async Task<IActionResult> GetTShirts([FromQuery] TShirtParameters tshirtParameters)
        {
            if (tshirtParameters is null)
            {
                _logger.LogError("TShirtParameters object send from client is null.");
                
                return BadRequest("TShirtParameters object is null.");
            }

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

                return Ok(result);
                //return NotFound();
            }

            _logger.LogInfo($"Received a t-shirt with id: {id}.");

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("getByUser")]
        public async Task<IActionResult> GetByUserId([FromQuery] TShirtParameters tshirtParameters)

        {
            if (tshirtParameters is null)
            {
                _logger.LogError("TShirtParameters object send from client is null.");
                
                return BadRequest("TShirtParameters object is null.");
            }

            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var tshirtsWithMetadata = await _tshirtService.GetAllByCurrentUserAsync(email, tshirtParameters);
            SetResponseHeaders(tshirtsWithMetadata.MetaData);
            var tshirts = _mapper.Map<IEnumerable<TShirtToReturnDTO>>(tshirtsWithMetadata);
            _logger.LogInfo($"Received a t-shirt for user with email: {email}.");

            return Ok(tshirts);
        }

        [HttpGet]
        [Route("getByAuthor/{name}")]
        public async Task<IActionResult> GetByAuthorName(string name, [FromQuery] TShirtParameters tshirtParameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError("User name send from client is null or empty.");

                return BadRequest("User name is null or empty.");
            }

            if (tshirtParameters is null)
            {
                _logger.LogError("TShirtParameters object send from client is null.");

                return BadRequest("TShirtParameters object is null.");
            }

            var tshirtsWithMetadata = await _tshirtService.GetByUserAsync(name, tshirtParameters);
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

            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var tshirt = await _tshirtService.CreateAsync(model, email);

            return Ok(tshirt);
        }
    }
}
