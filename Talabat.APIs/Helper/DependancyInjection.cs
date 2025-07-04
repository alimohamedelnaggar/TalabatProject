using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Mapping;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Repository.Data.Contexts;
using Talabat.Service.ProductsService;

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
            return services;
        } 
        private static IServiceCollection AddDbContextService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<TalabatDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
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
