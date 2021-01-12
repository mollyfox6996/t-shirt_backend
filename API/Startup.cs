using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Services.Interfaces;
using Services.Mappers;
using System.IO;
using Services.Services;
using LettuceEncrypt;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLoggerService();
            services.AddControllers();
            services.AddLettuceEncrypt().PersistDataToDirectory(new DirectoryInfo("lettuce"), null);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.ConfigureDbContext(Configuration);
            services.ConfigureRepositoryService();
            services.ConfigureBasketRepository();
            services.ConfigureCategoryService();
            services.ConfigureGenderService();
            services.ConfigureRedis(Configuration);
            services.ConfigureIdentity(Configuration);
            services.ConfigureTshirtService();
            services.ConfigureTokenService();
            services.ConfigureEmailService();
            services.ConfigureUserService();
            services.ConfigureCommentsService();
            services.ConfigureLikeService();
            services.ConfigureRatingService();
            services.AddSignalR();
            services.ConfigureBasketService();
            services.ConfigureOrderService();
            services.ConfigureSwagger();
            services.ConfigureCors(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerService logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TShirt API V1"));
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<AppHub>("/hub");
                });
        }
    }
}
