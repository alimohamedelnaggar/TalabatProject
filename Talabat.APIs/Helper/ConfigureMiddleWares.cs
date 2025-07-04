using Microsoft.EntityFrameworkCore;
using Talabat.APIs.MiddleWares;
using Talabat.Repository.Data.Contexts;

namespace Talabat.APIs.Helper
{
    public static class ConfigureMiddleWares
    {
        public async static Task<IApplicationBuilder> UseMiddleWaresServiceAsync(this WebApplication app)
        {
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

            app.UseMiddleware<ExceptionMiddleWare>();

            app.UseStatusCodePagesWithReExecute("/error/{0}");


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            return app;
        }

    }
}
