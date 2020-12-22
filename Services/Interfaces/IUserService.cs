using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Services.DTOs.LoginDTOs;
using Services.DTOs.OperationResultDTOs;
using Services.DTOs.UserDTOs;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> Register(UserForRegisterDTO model);
        Task<LoginResultDTO> LogIn(LoginDTO model);
        Task<UserForReturnDTO> GetUser(string email);
        Task SignOut();
        Task<OperationResultDTO<string>> ConfirmEmail(string userId, string code);
    }
}
