using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryManager
    {
        ITShirtRepository TShirt { get; }
        ICategoryRepository Category { get; }
        IGenderRepository Gender { get; }
        ICommentRepository Comment { get; }
        ILikeRepository Like { get; }
        IRatingRepository Rating { get; }
        Task SaveAsync();
    }
}
