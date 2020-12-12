using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Repository
{
    public class RatingRepository: RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void AddRating(Rating rating) => Create(rating);
       
        public async Task<Rating> Find(int shirtId, string userId) => await FindByCondition(i => i.ShirtId == shirtId && i.UserId == userId).FirstOrDefaultAsync();


        public async Task<IEnumerable<Rating>> GetRatingByShirtId(int id, bool trackChanges) => await FindByCondition(i => i.ShirtId == id, trackChanges).ToListAsync();


    }
}
