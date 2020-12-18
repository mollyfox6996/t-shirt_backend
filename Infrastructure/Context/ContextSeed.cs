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
                    var data = File.ReadAllText("../Infrastructure/SeedData/delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);

                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Genders.Any())
                {
                    var data = File.ReadAllText("../Infrastructure/SeedData/gender.json");
                    var genders = JsonSerializer.Deserialize<List<Gender>>(data);

                    foreach (var item in genders)
                    {
                        context.Genders.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Categories.Any())
                {
                    var data = File.ReadAllText("../Infrastructure/SeedData/category.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(data);

                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
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