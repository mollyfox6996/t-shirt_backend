using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IGenderService
    {
        Task<IEnumerable<Gender>> GetAllGendersAsync();
        Task<Gender> FindGenderAsync(Expression<Func<Gender, bool>> expression, bool trackChanges);
    }
}