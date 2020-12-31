using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryManager
    {
        ITshirtRepository Tshirt { get; }
        ICategoryRepository Category { get; }
        IGenderRepository Gender { get; }
        ICommentRepository Comment { get; }
        ILikeRepository Like { get; }
        IRatingRepository Rating { get; }
        IOrderRepository Order { get; }
        IDeliveryMethodRepository DeliveryMethod { get; } 
        Task SaveAsync();
    }
}
