using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Services.DTOs.RatingDTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IHubContext<AppHub> _hub;
        private readonly UserManager<AppUser> _userManager;

        public RatingService(IRepositoryManager repositoryManager, IHubContext<AppHub> hub, UserManager<AppUser> userManager)
        {
            _repositoryManager = repositoryManager;
            _hub = hub;
            _userManager = userManager;
        }

        public async Task AddRating(AddRatingDTO addRatingDto, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var rating = new Rating
            {
                AuthorName = user.DisplayName,
                UserId = user.Id,
                ShirtId = addRatingDto.ShirtId,
                Value = addRatingDto.Value
            };

            _repositoryManager.Rating.AddRating(rating);
            await _repositoryManager.SaveAsync();

            var ratingDto = await GetRatingByShirt(addRatingDto.ShirtId);
            await _hub.Clients.All.SendAsync("Rating Added", ratingDto);
        }

        public async Task<bool> CheckRatingFromUser(int shirtId, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var rating = await _repositoryManager.Rating.Find(shirtId, user.Id);

            return rating != null;
        }

        public async Task<RatingDTO> GetRatingByShirt(int id)
        {
            var rating = await _repositoryManager.Rating.GetRatingByShirtId(id, true);
            if (!rating.Any())
            {
                return new RatingDTO
                {
                    ShirtId = id,
                    AvgValue = 0
                };
            }
            
            var avg = rating.Average(n => n.Value);
            var ratingDto = new RatingDTO
            {
                ShirtId = id,
                AvgValue = avg
            };

            return ratingDto;
            
        }
    }
}
