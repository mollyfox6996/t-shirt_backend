using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
