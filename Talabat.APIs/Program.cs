
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Talabat.Repository.Data.Contexts;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TalabatDbContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();


            var scoped = app.Services.CreateScope();
            var service = scoped.ServiceProvider;
            var context = service.GetRequiredService<TalabatDbContext>();
           
            var loggerfactory = service.GetRequiredService<ILoggerFactory>();
            try
            {

                await context.Database.MigrateAsync();
                await SeedDataContext.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex.Message);

            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
