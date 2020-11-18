using Services.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
