using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

public class DeliveryMethodRepository : RepositoryBase<DeliveryMethod>, IDeliveryMethodRepository
{
    public DeliveryMethodRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync(bool trackChanges) =>  await FindAll(trackChanges).ToListAsync();
    public async Task<DeliveryMethod> GetDeliveryMethodAsync(int id) => await FindByCondition(i => i.Id == id, false).SingleOrDefaultAsync();

    public void CreateMethod(DeliveryMethod method) => Create(method);
    public void DeleteMethod(DeliveryMethod method) => Delete(method);
    
}
