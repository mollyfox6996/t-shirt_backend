using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        public LikeRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
        }

        public void CreateLike(Like like) => Create(like);

        public void DeleteLike(Like like) => Delete(like);
        public async Task<Like> Find(int shirtId, string userId) => await FindByCondition(i => i.ShirtId == shirtId && i.UserId == userId, false).FirstOrDefaultAsync();
        public async Task<IEnumerable<Like>> GetLikeByShirtId(int id, bool trackChanges) => await FindByCondition(i => i.ShirtId == id, trackChanges).ToListAsync();
    }
}
