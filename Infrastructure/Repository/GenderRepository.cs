using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GenderRepository : RepositoryBase<Gender>, IGenderRepository
    {
        public GenderRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Gender>> GelAllGendersAsync(bool trackChanges) => 
            await FindAll(trackChanges)
                .ToListAsync();

        public async Task<Gender> GetGenderByIdAsync(int id, bool trackChanges) => 
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<Gender> FindGenderAsync(Expression<Func<Gender, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges).SingleOrDefaultAsync();
    }
}
