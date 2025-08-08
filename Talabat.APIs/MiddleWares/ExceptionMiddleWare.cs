using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IWebHostEnvironment env;

        public string Details { get; set; }

        public ExceptionMiddleWare(RequestDelegate next,ILogger<ExceptionMiddleWare> logger,IWebHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = env.IsDevelopment() ?
                    new ExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace?.ToString())
                    : new ExceptionResponse(StatusCodes.Status500InternalServerError);
                var option=new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase };
               await context.Response.WriteAsync(JsonSerializer.Serialize(response,option));
                
            }

        }

    }
}
