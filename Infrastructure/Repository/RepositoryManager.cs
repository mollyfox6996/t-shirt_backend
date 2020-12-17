using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ITShirtRepository _tshirtRepository;
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

        public ITShirtRepository TShirt
        {
            get
            {
                if (_tshirtRepository is null)
                {
                    _tshirtRepository = new TShirtRepository(_repositoryContext);
                }

                return _tshirtRepository;
            }
        }


        public ICategoryRepository Category
        {
            get
            {
                if (_categoryRepository is null)
                {
                    _categoryRepository = new CategoryRepository(_repositoryContext);
                }

                return _categoryRepository;
            }
        }

        public IGenderRepository Gender
        {
            get
            {
                if (_genderRepository is null)
                {
                    _genderRepository = new GenderRepository(_repositoryContext);
                }

                return _genderRepository;
            }
        }

        public ICommentRepository Comment
        {
            get
            {
                if(_commentRepository is null)
                {
                    _commentRepository = new CommentRepository(_repositoryContext);
                }

                return _commentRepository;
            }
        }

        public ILikeRepository Like
        {
            get
            {
                if (_likeRepository is null)
                {
                    _likeRepository = new LikeRepository(_repositoryContext);
                }

                return _likeRepository;
            }
        }

        public IRatingRepository Rating
        {
            get
            {
                if (_ratingRepository is null)
                {
                    _ratingRepository = new RatingRepository(_repositoryContext);
                }

                return _ratingRepository;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if (_orderRepository is null)
                {
                    _orderRepository = new OrderRepository(_repositoryContext);
                }

                return _orderRepository;
            }
        }

        public IDeliveryMethodRepository DeliveryMethod
        {
            get
            {
                if (_deliveryMethodRepository is null)
                {
                    _deliveryMethodRepository = new DeliveryMethodRepository(_repositoryContext);
                }

                return _deliveryMethodRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
