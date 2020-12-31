using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICommentRepository
    {
        void CreateComment(Comment comment);
        Task<IEnumerable<Comment>> GetCommentByShirtIdAsync(int id, bool trackChanges);
    }
}
