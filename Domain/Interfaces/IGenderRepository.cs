using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenderRepository
    {
        Task<IEnumerable<Gender>> GelAllGendersAsync(bool trackChanges);
        Task<Gender> GetGenderByIdAsync(int id, bool trackChanges);
        Task<Gender> FindGenderAsync(Expression<Func<Gender, bool>> expression, bool trackChanges);
    }
}
