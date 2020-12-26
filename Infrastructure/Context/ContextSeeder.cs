using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Context
{
    public class ContextSeeder
    {
        public static async Task SeedAsync(RepositoryContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.DeliveryMethods.Any())
                {
                   
                    var methods = new DeliveryMethod[]
                    {
                        new DeliveryMethod {Name = "Courier", DeliveryTime = "2 days", Price = 350},
                        new DeliveryMethod {Name = "Standart Delivery", DeliveryTime = "7 days", Price = 140}
                    };

                    await context.DeliveryMethods.AddRangeAsync(methods);

                    await context.SaveChangesAsync();
                }

                if(!context.Genders.Any())
                {
                    var genders = new Gender[]
                    {
                        new Gender {Name = "Male"},
                        new Gender {Name = "Female"}
                    };

                    await context.SaveChangesAsync();
                }

                if(!context.Categories.Any())
                {

                    var categories = new Category[]
                    {
                        new Category {Name = "IT"},
                        new Category {Name = "Humor"},
                        new Category {Name = "Animals"},
                        new Category {Name = "Other"}
                    };

                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ContextSeeder>();
                logger.LogError(ex.Message);
            }
        }
    }
}