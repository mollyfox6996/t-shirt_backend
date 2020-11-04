using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> Register(UserDTO model);
        Task<LoginResultDTO> LogIn(LoginDTO model);
        Task<UserDTO> GetUser(string email);
        Task SignOut();

        Task<OperationResultDTO<string>> ConfirmEmail(string userId, string code);
        
    }
}
