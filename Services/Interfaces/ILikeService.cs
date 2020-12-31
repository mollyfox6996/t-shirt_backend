using System.Collections.Generic;
using System.Threading.Tasks;
using Services.DTOs.LikeDTOs;

namespace Services.Interfaces
{
    public interface ILikeService
    {
        Task AddLike(int shirtId, string email);
        Task DeleteLike(int shirtId, string email);
        Task<IEnumerable<LikeDTO>> GetLikesByShirt(int id);
        Task<bool> CheckLikeFromUser(int shirtId, string email);
    }
}
