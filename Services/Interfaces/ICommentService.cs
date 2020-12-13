using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICommentService
    {
        Task AddComent(CreateCommentDTO createCommentDTO, string email);
        Task<IEnumerable<CommentDTO>> GetTshirtComments(int shirtId);
    }
}
