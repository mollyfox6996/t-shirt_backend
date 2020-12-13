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
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateComment(Comment comment) => Create(comment);

        public async Task<IEnumerable<Comment>> GetCommentByShirtIdAsync(int id, bool trackChanges) => await FindByCondition(i => i.ShirtId == id, trackChanges).ToListAsync();
    }
}
