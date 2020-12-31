using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILikeRepository
    {
        void CreateLike(Like like);
        void Delete(Like like);
        Task<Like> Find(int shirtId, string userId);
        Task<IEnumerable<Like>> GetLikeByShirtId(int id, bool trackChanges);

    }
}
