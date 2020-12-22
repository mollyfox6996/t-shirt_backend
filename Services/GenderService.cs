using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class GenderService : IGenderService
    {
        private readonly IRepositoryManager _repositoryManager;

        public GenderService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<Gender>> GetAllGendersAsync() =>
            await _repositoryManager.Gender.GelAllGendersAsync();
        
        public async Task<Gender> FindGenderAsync(Expression<Func<Gender, bool>> expression, bool trackChanges) =>
            await _repositoryManager.Gender.FindGenderAsync(expression, trackChanges);
    }
}