﻿using Infrastructure.Context;
using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.Entities;
using System.Text;
using Microsoft.OpenApi.Models;
using Domain.Interfaces;
using Infrastructure.Repository;
using Services.Services;
using StackExchange.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureEmailService(this IServiceCollection services) => services.AddScoped(typeof(IEmailService), typeof(EmailService));
        public static void ConfigureTokenService(this IServiceCollection services) => services.AddScoped(typeof(ITokenService), typeof(TokenService));
        public static void ConfigureUserService(this IServiceCollection services) => services.AddScoped(typeof(IUserService), typeof(UserService));
        public static void ConfigureTshirtService(this IServiceCollection services) => services.AddScoped(typeof(ITshirtService), typeof(TshirtService));
        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddScoped<ILoggerService, LoggerService>();
        public static void ConfigureBasketService(this IServiceCollection services) => services.AddScoped(typeof(IBasketService), typeof(BasketService));
        public static void ConfigureLikeService(this IServiceCollection services) => services.AddScoped(typeof(ILikeService), typeof(LikeService));
        public static void ConfigureRatingService(this IServiceCollection services) => services.AddScoped(typeof(IRatingService), typeof(RatingService));
        public static void ConfigureCommentsService(this IServiceCollection services) => services.AddScoped(typeof(ICommentService), typeof(CommentService));
        public static void ConfigureOrderService(this IServiceCollection services) => services.AddScoped(typeof(IOrderService), typeof(OrderService));
        public static void ConfigureCategoryService(this IServiceCollection services) => services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
        public static void ConfigureGenderService(this IServiceCollection services) => services.AddScoped(typeof(IGenderService), typeof(GenderService));
        public static void ConfigureRepositoryService(this IServiceCollection services) => services.AddScoped(typeof(IRepositoryManager), typeof(RepositoryManager));
        public static void ConfigureBasketRepository(this IServiceCollection services) => services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
        public static void ConfigureRedis(this IServiceCollection services,IConfiguration configuration) => services.AddSingleton<IConnectionMultiplexer>(c => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
        
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRoles<IdentityRole>();
            builder.AddEntityFrameworkStores<RepositoryContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();
            builder.AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 4;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecretKey"])),
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });
        }

        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(configuration["AngularUrl"], configuration["NginxProxyUrl"])
                    .AllowCredentials();
                });
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TShirt API", Version = "V1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT authentication scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement {
                    {securitySchema, new[] { "Bearer"}}};
                c.AddSecurityRequirement(securityRequirement);
            });
        }
    }
}
