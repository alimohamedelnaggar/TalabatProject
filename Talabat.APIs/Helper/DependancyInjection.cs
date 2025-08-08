using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Identity;
using Talabat.Core.Mapping;
using Talabat.Core.Mapping.Auth;
using Talabat.Core.Mapping.Basket;
using Talabat.Core.Mapping.Orders;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Repository.Data.Contexts;
using Talabat.Repository.Identity.Contexts;
using Talabat.Repository.Repositories;
using Talabat.Service.CacheService;
using Talabat.Service.Order;
using Talabat.Service.ProductsService;
using Talabat.Service.Token;
using Talabat.Service.Users;

namespace Talabat.APIs.Helper
{
    public static class DependencyInjection
    {
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        } 
        public static IServiceCollection AddDependencyService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddDbContextService(configuration);
            services.AddSwaggerService();
            services.AddUserDefinedService();
            services.AddAutoMapperService(configuration);
            services.AddConfigureInvalidModelStateResponseService();
            services.AddRedisService(configuration);
            services.AddIdentityService();
            services.AddAuthenticationService(configuration);
            services.AddGoogleAuthenticationService(configuration);
            return services;
        } 
        private static IServiceCollection AddDbContextService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<TalabatDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<StoreIdentityDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            return services;
        }
        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddRedisService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection= configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            return services;
        }
        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
        private static IServiceCollection AddGoogleAuthenticationService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication().AddGoogle(op =>
            {
                IConfigurationSection googleAuthSection = configuration.GetSection("Auth:Google");
                op.ClientId = googleAuthSection["ClientId"];
                op.ClientSecret = googleAuthSection["ClientSecret"];
            });
            return services;
        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });
            
            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));
            services.AddAutoMapper(m => m.AddProfile(new AuthProfile()));
            services.AddAutoMapper(m => m.AddProfile(new OrderProfile(configuration)));
            return services;
        }
        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }
        private static IServiceCollection AddConfigureInvalidModelStateResponseService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(op =>
            {
                op.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                     .SelectMany(p => p.Value.Errors)
                     .Select(p => p.ErrorMessage);
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };




            });

            return services;
        }


    }
}
