using System.Threading.Tasks;
using Services.DTOs.RatingDTOs;

namespace Services.Interfaces
{
    public interface IRatingService
    {
        Task AddRating(AddRatingDTO addRatingDTO, string email);
        Task<RatingDTO> GetRatingByShirt(int id);
        Task<bool> CheckRatingFromUser(int shirtId, string email);
    }
}
