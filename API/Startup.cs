using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using Services.Interfaces;
using Services.Mappers;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLoggerService();
            services.AddControllers();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.ConfigureDbContext(Configuration);
<<<<<<< HEAD
            services.ConfigureRepositoryService();
            //services.ConfigureTshirtRepository();
            //services.ConfigureBasketRepository();
=======
            services.ConfigureTshirtRepository();
            services.ConfigureBasketRepository();
>>>>>>> refs/remotes/origin/dev
            services.ConfigureIdentity(Configuration);
            services.ConfigureTshirtService();
            services.ConfigureTokenService();
            services.ConfigureEmailService();
            services.ConfigureUserService();
            services.ConfigureBasketService();
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
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
