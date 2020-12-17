using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class DeliveryMethodRepository : RepositoryBase<DeliveryMethod>, IDeliveryMethodRepository
{
    public DeliveryMethodRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync() =>  await FindAll(true).ToListAsync();
    
}
