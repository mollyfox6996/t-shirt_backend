using Domain.Entities;
using Domain.Interfaces;
using Domain.RequestFeatures;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TShirtRepository : ITshirtRepository
    {
        private readonly AppDbContext _context;

        public TShirtRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateTShirtAsync(TShirt shirt)
        {
             await _context.TShirts.AddAsync(shirt);
             _context.SaveChanges();
        }

        public async Task<TShirt> GetTShirtByIdAsync(int id) => await _context.TShirts.Include(p => p.Category).Include(p => p.Gender).Include(p => p.User).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<PagedList<TShirt>> GetTShirtListAsync(TShirtParameters tshirtParameters) => PagedList<TShirt>.ToPagedList(await _context.TShirts.ToListAsync(), tshirtParameters.PageNumber, tshirtParameters.PageSize);

        public async Task<IEnumerable<TShirt>> GetTshirtByCurrentUserAsync(string userId) => await _context.TShirts.Where(x => x.UserId == userId).Include(p => p.Category).Include(p => p.Gender).Include(p => p.User).ToListAsync();

        public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _context.Categories.ToListAsync();

        public async Task<Category> GetCategoryAsync(string name) => await _context.Categories.FirstOrDefaultAsync(p => p.Name == name);
        public async Task<IEnumerable<Gender>> GetGendersAsync() => await _context.Genders.ToListAsync();
        public async Task<Gender> GetGenderAsync(string name) => await _context.Genders.FirstOrDefaultAsync(p => p.Name == name);
        public async Task<IEnumerable<TShirt>> GetByAuthorAsync(string authorName) => await _context.TShirts.Where(p => p.User.DisplayName == authorName).Include(p => p.Category).Include(p => p.Gender).Include(p => p.User).ToListAsync();

    }
}
