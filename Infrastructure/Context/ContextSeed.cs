using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Context
{
    public class ContextSeed
    {
        public static async Task SeedAsync(RepositoryContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.DeliveryMethods.Any())
                {
                    var data = await File.ReadAllTextAsync("../Infrastructure/SeedData/delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);

                    foreach (var item in methods)
                    {
                        await context.DeliveryMethods.AddAsync(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Genders.Any())
                {
                    var data = await File.ReadAllTextAsync("../Infrastructure/SeedData/gender.json");
                    var genders = JsonSerializer.Deserialize<List<Gender>>(data);

                    foreach (var item in genders)
                    {
                        await context.Genders.AddAsync(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Categories.Any())
                {
                    var data = await File.ReadAllTextAsync("../Infrastructure/SeedData/category.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(data);

                    foreach (var item in categories)
                    {
                        await context.Categories.AddAsync(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}