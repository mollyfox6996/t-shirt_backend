using Domain.Entities;
using Domain.Interfaces;
using Domain.RequestFeatures;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Infrastructure.Extensions;

namespace Infrastructure.Repository
{
    public class TshirtRepository : RepositoryBase<TShirt>, ITshirtRepository
    {
        public TshirtRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<TShirt>> GetTShirtListAsync(TshirtParameters tshirtParameters, bool trackChanges) => 
            PagedList<TShirt>
                .ToPagedList(await FindAll(trackChanges)
                .Filter(tshirtParameters)
                .Sort(tshirtParameters.OrderBy)
                .Search(tshirtParameters.SearchTerm)
                .Include(c => c.Category)
                .Include(g => g.Gender)
                .Include(u => u.User)
                .ToListAsync(), 
                tshirtParameters.PageNumber, 
                tshirtParameters.PageSize);

        public async Task<TShirt> GetTShirtByIdAsync(int id, bool trackChanges) => 
            await FindByCondition(c => c.Id == id, trackChanges)
                .Include(c => c.Category)
                .Include(g => g.Gender)
                .Include(u => u.User)
                .SingleOrDefaultAsync();

        public void CreateTShirt(TShirt shirt) => Create(shirt);
     
        public void UpdateTShirt(TShirt shirt) => Update(shirt);

        public void DeleteTShirt(TShirt shirt) => Delete(shirt);
    }
}
