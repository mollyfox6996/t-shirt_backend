using Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoggerService _logger;

        public AccountController(IUserService userService, ILoggerService logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO model)
        {
            if (model is null)
            {
                _logger.LogError("UserForRegisterDTO object send from client is null");
                return BadRequest("UserForRegisterDTO object is null");
            }

            var result = await _userService.Register(model);
           
            if (!result.Succeeded)
            {
                _logger.LogInfo($"User {model.DisplayName} didn't successfully register.");
                
                return Ok(result);
                //return BadRequest(result.Errors);
            }

            _logger.LogInfo($"User {model.DisplayName} has successfully register.");

            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (model is null)
            {
                _logger.LogError("LoginDTO object send from client is null");
                return BadRequest("LoginDTO object is null");
            }

            var result = await _userService.LogIn(model);

            if (!result.Success)
            {
                _logger.LogError($"User with email {model.Email} didn't successfully log.");

                return Ok(result);
                //return BadRequest(result.Message);
            }

            _logger.LogInfo($"User with {model.Email} has successfully logged.");

            return Ok(result);
        }

        [HttpGet]
        [Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _userService.SignOut();
            _logger.LogInfo("User logged out.");

            return Ok();
        }

        [HttpGet]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var result = await _userService.ConfirmEmail(userId, code);
           
            if(!result.Success)
            {
                _logger.LogError("Unsuccessful email confirmation.");
                return BadRequest(result.Message);
            }

            _logger.LogInfo("Successful email confirmation.");
            
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("getCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var user = await _userService.GetUser(email);
            _logger.LogInfo($"Get current user with email {email}.");

            return Ok(user);
        }
    }
}
