using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRatingService
    {
        Task AddRating(AddRatingDTO addRatingDTO, string email);
        Task<RatingDTO> GetRatingByShirt(int id);
        Task<bool> CheckRatingFromUser(int shirtId, string email);
    }
}
