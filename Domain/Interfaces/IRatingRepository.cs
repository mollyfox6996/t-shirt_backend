using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRatingRepository
    {
        void AddRating(Rating rating);
        void Delete(Rating rating);
        Task<Rating> Find(int shirtId, string userId);
        Task<IEnumerable<Rating>> GetRatingByShirtId(int id, bool trackChanges);
    }
}
