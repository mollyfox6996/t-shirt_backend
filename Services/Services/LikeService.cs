using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Services.DTOs.LikeDTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class LikeService : ILikeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IHubContext<AppHub> _hub;
        private readonly UserManager<AppUser> _userManager;

        public LikeService(IRepositoryManager repositoryManager, IMapper mapper, IHubContext<AppHub> hub, UserManager<AppUser> userManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _hub = hub;
            _userManager = userManager;
        }

        public async Task AddLike(int shirtId, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var likeDto = new LikeDTO
            {
                AuthorName = user.DisplayName,
                ShirtId = shirtId
            };

            var like = _mapper.Map<Like>(likeDto);
            like.UserId = user.Id;

            _repositoryManager.Like.CreateLike(like);
            await _repositoryManager.SaveAsync();

            await _hub.Clients.All.SendAsync("Like Added", like);
        }

        public async Task DeleteLike(int shirtId, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var like = await _repositoryManager.Like.Find(shirtId, user.Id);

            if (like != null)
            {
                _repositoryManager.Like.Delete(like);
                await _repositoryManager.SaveAsync();
                await _hub.Clients.All.SendAsync("Like Deleted");
            }
        }

        public async Task<IEnumerable<LikeDTO>> GetLikesByShirt(int id)
        {
            var likes = await _repositoryManager.Like.GetLikeByShirtId(id, true);
            return _mapper.Map<IEnumerable<LikeDTO>>(likes);
        }

        public async Task<bool> CheckLikeFromUser(int shirtId, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var like = await _repositoryManager.Like.Find(shirtId, user.Id);

            return like != null;
        }
    }
}
