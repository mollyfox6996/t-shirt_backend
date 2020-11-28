using Services.DTOs;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        public async Task<OperationResultDTO<string>> ConfirmEmail(string userId, string code)
        {
            OperationResultDTO<string> result = new OperationResultDTO<string>();
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            IdentityOptions options = new IdentityOptions();

            if (userId == null || code == null)
            {
                result.Success = false;
                result.Message = "There is some problem. We are not able to activate your account.";
            }
            var proivder = options.Tokens.EmailConfirmationTokenProvider;

            bool isValid = await _userManager.VerifyUserTokenAsync(user, proivder, "EmailConfirmation", HttpUtility.UrlDecode(code));

            if (isValid)
            {
                var data = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(code));
                if (data.Succeeded)
                {
                    result.Message = "Your account has been activated successfully.";
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Your account confirmation link has been expired or invalid link, Please contact to administrator.";
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Your account confirmation link has been expired or invalid link.";
            }
            return result;
        }

        public async Task<IdentityResult> Register(UserForRegisterDTO model)
        {
            var user = new AppUser
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                UserName = model.Email
                
            };

            var userResult = await _userManager.CreateAsync(user, model.Password);

            if(userResult.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var url = "http://localhost:4200/account/accountConfirm";
                var displayName = user.DisplayName;
                var callbackUrl = url + "?userId=" + user.Id + "&code=" + HttpUtility.UrlEncode(code);
                var text =  $"<h1>Hi, {displayName}.</h1><p>Click on the link to confirm you email adress {callbackUrl}</p>";

                await _emailService.SendEmail(user.Email, HtmlEncoder.Default.Encode(callbackUrl), "Activation link", text);


            }
            return userResult;
        }

        public async Task<LoginResultDTO> LogIn(LoginDTO model)
        {
            var loginResult = new LoginResultDTO();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user != null)
            {
                if(user.EmailConfirmed == false)
                {
                    loginResult.Success = false;
                    loginResult.Message = "You account not activated";

                    return loginResult;
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

            if(result.Succeeded)
            {
                var token = _tokenService.CreateToken(user);

                loginResult.Success = true;
                loginResult.Token = token;
            }
            else
            {
                loginResult.Success = false;
                loginResult.Message = "Username or password is incorrect.";
            }

            return loginResult;

        }

        public async Task<UserForReturnDTO> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            UserForReturnDTO userResult = new UserForReturnDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                
            };
            return userResult;

        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
