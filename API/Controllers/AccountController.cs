using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IdentityResult> Register(User model) => await _userService.Create(model);

        [HttpPost]
        [Route("login")]
        public async Task<LoginResult> Login(Login model) => await _userService.SignIn(model);

        [HttpGet]
        [Route("signout")]
        public async Task SignOut() => await _userService.SignOut();

        [HttpGet]
        [Route("confirmEmail")]
        public async Task<OperationResult<string>> ConfirmEmail(string userId, string code)
        {
            OperationResult<string> result = new OperationResult<string>();
            result = await _userService.ConfirmEmail(userId, code);
            return result;
        }
    }
}
