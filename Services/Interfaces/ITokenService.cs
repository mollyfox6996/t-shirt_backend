using Domain.Entities;

namespace Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}