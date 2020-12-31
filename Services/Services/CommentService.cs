using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Services.DTOs.CommentDTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IHubContext<AppHub> _hub;
        private readonly UserManager<AppUser> _userManager;

        public CommentService(IRepositoryManager repositoryManager, IMapper mapper, IHubContext<AppHub> hub, UserManager<AppUser> userManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _hub = hub;
            _userManager = userManager;
        }

        public async Task AddComment(CreateCommentDTO createCommentDTO, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var commentDTO = new CommentDTO
            {
                AuthorName = user.DisplayName,
                ShirtId = createCommentDTO.ShirtId, 
                Text = createCommentDTO.Text
            };
            
            var comment = _mapper.Map<Comment>(commentDTO);
            _repositoryManager.Comment.CreateComment(comment);
            await _repositoryManager.SaveAsync();
            await _hub.Clients.All.SendAsync("Add", commentDTO);
        }

        public async Task<IEnumerable<CommentDTO>> GetTshirtComments(int shirtId)
        {
            var comments = await _repositoryManager.Comment.GetCommentByShirtIdAsync(shirtId, true);
            
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }
    }
}
