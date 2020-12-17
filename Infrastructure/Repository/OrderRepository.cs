using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(RepositoryContext repositoryContext): base(repositoryContext)
    {

    }
    public void CreateOrder(Order order) => Create(order);
    

    public async Task<Order> GetOrderByIdAsync(int id, string email) => await FindByCondition(i => i.Id == id && i.Email == email).SingleOrDefaultAsync();
    

    public async Task<IEnumerable<Order>> GetOrdersForUserAsync(string email) => await FindByCondition(i => i.Email == email, true).ToListAsync();
    
}
