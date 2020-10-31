using Domain;
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
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _configuration;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<OperationResult<string>> ConfirmEmail(string userId, string code)
        {
            OperationResult<string> result = new OperationResult<string>();
            AppUser user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
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

        public async Task<IdentityResult> Create(User model)
        {
            var user = new AppUser
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                UserName = model.Email
                
            };

            var userResulr = await _userManager.CreateAsync(user, model.Password);

            if(userResulr.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var url = "http://localhost:4200/Account/accountActivation";

                var callbackUrl = url + "?userId=" + user.Id + "&code=" + HttpUtility.UrlEncode(code);
                await _emailService.SendEmail(user.Email, user.DisplayName, HtmlEncoder.Default.Encode(callbackUrl), "Activation link");


            }
            return userResulr;
        }

        public async Task<LoginResult> SignIn(Login model)
        {
            var loginResult = new LoginResult();
            var user = await _userManager.FindByNameAsync(model.Email);

            if(user != null)
            {
                if(user.EmailConfirmed == false)
                {
                    loginResult.Success = false;
                    loginResult.Message = "You account not activated";

                    return loginResult;
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if(result.Succeeded)
            {
                var secretKey = _configuration["SecretKey"];
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())

                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

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

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
