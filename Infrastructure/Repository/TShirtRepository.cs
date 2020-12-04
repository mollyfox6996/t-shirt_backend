using Domain.Entities;
using Domain.Interfaces;
using Domain.RequestFeatures;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                .ToPagedList(await FindAll(trackChanges)
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
    }
}
