using System.Collections.Generic;
using System.Threading.Tasks;
using Services.DTOs.CommentDTOs;

namespace Services.Interfaces
{
    public interface ICommentService
    {
        Task AddComment(CreateCommentDTO createCommentDTO, string email);
        Task<IEnumerable<CommentDTO>> GetTshirtComments(int shirtId);
    }
}
