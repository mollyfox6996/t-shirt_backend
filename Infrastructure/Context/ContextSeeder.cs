using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Context
{
    public class ContextSeeder
    {
        public static async Task SeedAsync(RepositoryContext context, ILoggerFactory loggerFactory, UserManager<AppUser> userManager)
        {
            try
            {
                if(!userManager.Users.Any())
                {
                    var user1 = new AppUser
                    {
                        Email = "some@gmail.com",
                        DisplayName = "Molly", 
                        UserName = "some@gmail.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user1, "Aa__1239053");

                    var user2 = new AppUser
                    {
                        Email = "somenew@gmail.com",
                        DisplayName = "Toivo",
                        UserName = "somenew@gmail.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user2, "Btwe__343855k");
                    
                    var user3 = new AppUser
                    {
                        Email = "newsome@gmail.com",
                        DisplayName = "Albert", 
                        UserName = "newsome@gmail.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user3, "Rsa__1239u053");

                    var user4 = new AppUser
                    {
                        Email = "arma@gmail.com",
                        DisplayName = "Maruv",
                        UserName = "arma@gmail.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user4, "SammyBew__343855k");
                    
                }
                
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
                    
                    await context.Genders.AddRangeAsync(genders);
                    
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

                    await context.Categories.AddRangeAsync(categories);
                    
                    await context.SaveChangesAsync();
                }

                if (!context.TShirts.Any())
                {
                    var shirts = new TShirt[]
                    {
                        new TShirt
                        {
                            CategoryId = 1,
                            CreateDate = DateTime.Now,
                            GenderId = 1,
                            Description = "Shirt with print",
                            Name = "Super Dino",
                            Price = 350,
                            PictureUrl =
                                "https://res.cloudinary.com/dr2lzz3ap/image/upload/v1609149828/zgmxp8flitfpv3s6428a.png",
                            User = await userManager.FindByEmailAsync("some@gmail.com")
                        },
                        new TShirt
                        {
                            CategoryId = 1,
                            CreateDate = DateTime.Now,
                            GenderId = 2,
                            Description = "Shirt with print",
                            Name = "IT Academy",
                            Price = 114.2m,
                            PictureUrl =
                                "https://res.cloudinary.com/dr2lzz3ap/image/upload/v1609150729/cdxtlgzxy10oobxg2gpw.png",
                            User = await userManager.FindByEmailAsync("some@gmail.com")
                        },
                        new TShirt
                        {
                            CategoryId = 2, 
                            CreateDate = DateTime.Now, 
                            GenderId = 1, 
                            Description = "Shirt with print", 
                            Name = "Bones", 
                            Price = 99.2m, 
                            PictureUrl = "https://res.cloudinary.com/dr2lzz3ap/image/upload/v1609150467/va2paztwlomtttrtxwe7.png", 
                            User = await userManager.FindByEmailAsync("somenew@gmail.com")
                        },
                        new TShirt
                        {
                            CategoryId = 3, 
                            CreateDate = DateTime.Now, 
                            GenderId = 2, 
                            Description = "Shirt with print", 
                            Name = "Fights animals", 
                            Price = 124.99m, 
                            PictureUrl = "https://res.cloudinary.com/dr2lzz3ap/image/upload/v1609150526/fhrfsuswrdbxyttxpahg.png", 
                            User = await userManager.FindByEmailAsync("somenew@gmail.com")
                        },
                        new TShirt
                        {
                            CategoryId = 2, 
                            CreateDate = DateTime.Now, 
                            GenderId = 1, 
                            Description = "Shirt with print", 
                            Name = "Humor shirt", 
                            Price = 12.5m, 
                            PictureUrl = "https://res.cloudinary.com/dr2lzz3ap/image/upload/v1609150028/p3g6nhnyeal5atxysgtp.png", 
                            User = await userManager.FindByEmailAsync("newsome@gmail.com")
                        },
                        new TShirt
                        {
                            CategoryId = 2, 
                            CreateDate = DateTime.Now, 
                            GenderId = 2, 
                            Description = "Shirt with print", 
                            Name = "Humor shirt", 
                            Price = 235, 
                            PictureUrl = "https://res.cloudinary.com/dr2lzz3ap/image/upload/v1609150138/c0j0kzokm418svndnwwz.png", 
                            User = await userManager.FindByEmailAsync("newsome@gmail.com")
                        },
                        new TShirt
                        {
                            CategoryId = 2, 
                            CreateDate = DateTime.Now, 
                            GenderId = 1, 
                            Description = "Shirt with print", 
                            Name = "Humor shirt", 
                            Price = 12.5m, 
                            PictureUrl = "https://res.cloudinary.com/dr2lzz3ap/image/upload/v1609150807/zajrpm1ywf6asydbjhdn.png", 
                            User = await userManager.FindByEmailAsync("arma@gmail.com")
                        },
                        new TShirt
                        {
                            CategoryId = 2, 
                            CreateDate = DateTime.Now, 
                            GenderId = 2, 
                            Description = "Shirt with print", 
                            Name = "Humor shirt", 
                            Price = 235, 
                            PictureUrl = "https://res.cloudinary.com/dr2lzz3ap/image/upload/v1609150224/umwg9cuyetz7lgf9jftg.png", 
                            User = await userManager.FindByEmailAsync("arma@gmail.com")
                        },
                    };

                    await context.TShirts.AddRangeAsync(shirts);

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