using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenderRepository
    {
        Task<IEnumerable<Gender>> GelAllGendersAsync();
        Task<Gender> FindGenderAsync(Expression<Func<Gender, bool>> expression, bool trackChanges);
    }
}
