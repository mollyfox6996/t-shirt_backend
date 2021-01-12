using System;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using LettuceEncrypt;
using System.Net;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host =  CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var configuration = services.GetRequiredService<IConfiguration>();

                try 
                {
                    var context = services.GetRequiredService<RepositoryContext>();
                    await context.Database.MigrateAsync();
                    await ContextSeeder.SeedAsync(context, loggerFactory, userManager);
                    await RoleInitializer.InitializeAsync(userManager, rolesManager, configuration);
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogInformation("Ok"); 
                    
                }
                catch(Exception ex)
                {
                   var logger = loggerFactory.CreateLogger<Program>();
                   logger.LogError($"An error occurred during migration: {ex.Message}, {ex.StackTrace}"); 
                }
            }
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.PreferHostingUrls(false);
                    webBuilder.UseKestrel(k => 
                    {
                        var appServices = k.ApplicationServices;
                        k.Listen(IPAddress.Any, 443,
                        o => o.UseHttps(h => h.UseLettuceEncrypt(appServices)));
                            
                    });
                });
    }
}
