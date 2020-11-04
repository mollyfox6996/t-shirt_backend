using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IdentityResult> Register(UserDTO model) => await _userService.Register(model);

        [HttpPost]
        [Route("login")]
        public async Task<LoginResultDTO> Login(LoginDTO model) => await _userService.LogIn(model);

        [HttpGet]
        [Route("signout")]
        public async Task SignOut() => await _userService.SignOut();

        [HttpGet]
        [Route("confirmEmail")]
        public async Task<OperationResultDTO<string>> ConfirmEmail(string userId, string code)
        {
            OperationResultDTO<string> result = await _userService.ConfirmEmail(userId, code);
            return result;
        }

        [Authorize]
        [HttpGet]
        [Route("getCurrentUser")]
        public async Task<UserDTO> GetCurrentUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return await _userService.GetUser(email);
        }

    }
}
