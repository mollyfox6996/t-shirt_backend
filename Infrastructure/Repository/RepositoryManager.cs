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
        private IBasketRepository _basketRepository;
        private ICategoryRepository _categoryRepository;
        private IGenderRepository _genderRepository;
        private ICommentRepository _commentRepository;

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

        public IBasketRepository Basket
        {
            get
            {
                if (_basketRepository is null)
                {
                    _basketRepository = new BasketRepository(_repositoryContext);
                }

                return _basketRepository;
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

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
