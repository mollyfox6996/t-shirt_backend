using Domain.Entities;
using Domain.Interfaces;
using Domain.RequestFeatures;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class TShirtRepository : RepositoryBase<TShirt>, ITShirtRepository
    {
        public TShirtRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<TShirt>> GetTShirtListAsync(TShirtParameters tshirtParameters, bool trackChanges) => 
            PagedList<TShirt>
                .ToPagedList(await FindByCondition(Filter(tshirtParameters), trackChanges)
                .Include(c => c.Category)
                .Include(g => g.Gender)
                .Include(u => u.User)
                .ToListAsync(), 
                tshirtParameters.PageNumber, 
                tshirtParameters.PageSize);

        public async Task<PagedList<TShirt>> GetTshirtsByUserAsync(string userId, TShirtParameters tshirtParameters, bool trackChanges ) => 
            PagedList<TShirt>
                .ToPagedList(await FindByCondition(c => c.UserId == userId, trackChanges)
                .Include(c => c.Category)
                .Include(g => g.Gender)
                .Include(u => u.User)
                .ToListAsync(), 
                tshirtParameters.PageNumber, 
                tshirtParameters.PageSize);

        public async Task<TShirt> GetTShirtByIdAsync(int id, bool trackChanges) => 
            await FindByCondition(c => c.Id == id, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateTShirt(TShirt shirt) => Create(shirt);
     
        public void UpdateTShirt(TShirt shirt) => Update(shirt);

        public void DeleteTShirt(TShirt shirt) => Delete(shirt);

        private Expression<Func<TShirt, bool>> Filter (TShirtParameters tshirtParameters)
        {
            Expression<Func<TShirt, bool>> filter = c => 
            (!string.IsNullOrWhiteSpace(tshirtParameters.Gender) ? c.Gender.Name == tshirtParameters.Gender : true) && 
            (!string.IsNullOrWhiteSpace(tshirtParameters.Category) ? c.Category.Name == tshirtParameters.Category : true);
            
            return filter;
        }
    }
}
