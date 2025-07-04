using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.APIs.MiddleWares;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Mapping;
using Talabat.Core.Repostories.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Repository.Data.Contexts;
using Talabat.Service.ProductsService;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            builder.Services.AddDependencyService(builder.Configuration);

            //builder.Services.AddScoped<typeof (IGenericRepository<>),typeof(GenericRepository<>)>();
           
          

           


            var app = builder.Build();

           await app.UseMiddleWaresServiceAsync();
            

            app.Run();
        }
    }
}
