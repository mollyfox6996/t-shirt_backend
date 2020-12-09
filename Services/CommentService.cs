﻿using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IHubContext<AppHub> _hub;

        public CommentService(IRepositoryManager repositoryManager, IMapper mapper, IHubContext<AppHub> hub)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _hub = hub;
        }

        public async Task AddComent(CommentDTO commentDTO)
        {

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
