using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Services
{
    public class RatingService : IRatingService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IHubContext<AppHub> _hub;
        private readonly UserManager<AppUser> _userManager;

        public RatingService(IRepositoryManager repositoryManager, IMapper mapper, IHubContext<AppHub> hub, UserManager<AppUser> userManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _hub = hub;
            _userManager = userManager;
        }

        public async Task AddRating(AddRatingDTO addRatingDTO, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var rating = new Rating
            {
                AuthorName = user.DisplayName,
                UserId = user.Id,
                ShirtId = addRatingDTO.ShirtId,
                Value = addRatingDTO.Value
            };

            _repositoryManager.Rating.AddRating(rating);
            await _repositoryManager.SaveAsync();

            var ratingDTO = await GetRatingByShirt(addRatingDTO.ShirtId);
            await _hub.Clients.All.SendAsync("Rating Added", ratingDTO);

        }

        public async Task<bool> CheckRatingFromUser(int shirtId, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var rating = await _repositoryManager.Rating.Find(shirtId, user.Id);

            if (rating != null)
            {
                return true;
            }
            return false;
        }

        //TODO: Fix bug in rating.Average!!!!
        public async Task<RatingDTO> GetRatingByShirt(int id)
        {
            var ratings = await _repositoryManager.Rating.GetRatingByShirtId(id, true);
            if(ratings == null)
            {
                var rating = new RatingDTO
                {
                    ShirtId = id,
                    AvgValue = 0
                };
                return rating;
            }
            try
            {
                double avg = ratings.Average(n => n.Value);
                var ratingDTO = new RatingDTO
                {
                    ShirtId = id,
                    AvgValue = avg
                };
                return ratingDTO;
            }
            catch(Exception ex)
            {
                var ratingDTO = new RatingDTO
                {
                    ShirtId = id,
                    AvgValue = 0
                };
                return ratingDTO;
            }
            
        }
    }
}
