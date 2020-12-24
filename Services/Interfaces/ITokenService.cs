using Domain.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity; 

namespace Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager);
    }
}