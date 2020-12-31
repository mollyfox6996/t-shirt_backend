using Domain.Interfaces;
using Infrastructure.Context;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private ITshirtRepository _tshirtRepository;
        private ICategoryRepository _categoryRepository;
        private IGenderRepository _genderRepository;
        private ICommentRepository _commentRepository;
        private ILikeRepository _likeRepository;
        private IRatingRepository _ratingRepository;
        private IOrderRepository _orderRepository;
        private IDeliveryMethodRepository _deliveryMethodRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ITshirtRepository Tshirt
        {
            get { return _tshirtRepository ??= new TshirtRepository(_repositoryContext); }
        }

        public ICategoryRepository Category
        {
            get { return _categoryRepository ??= new CategoryRepository(_repositoryContext); }
        }

        public IGenderRepository Gender
        {
            get { return _genderRepository ??= new GenderRepository(_repositoryContext); }
        }

        public ICommentRepository Comment
        {
            get { return _commentRepository ??= new CommentRepository(_repositoryContext); }
        }

        public ILikeRepository Like
        {
            get { return _likeRepository ??= new LikeRepository(_repositoryContext); }
        }

        public IRatingRepository Rating
        {
            get { return _ratingRepository ??= new RatingRepository(_repositoryContext); }
        }

        public IOrderRepository Order
        {
            get { return _orderRepository ??= new OrderRepository(_repositoryContext); }
        }

        public IDeliveryMethodRepository DeliveryMethod
        {
            get { return _deliveryMethodRepository ??= new DeliveryMethodRepository(_repositoryContext); }
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
