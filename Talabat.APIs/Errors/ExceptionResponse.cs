using Talabat.APIs.Error;

namespace Talabat.APIs.Errors
{
    public class ExceptionResponse:ApiErrorResponse
    {
        public ExceptionResponse(int statusCode, string? errorMessage = null,string? details=null) : base(statusCode, errorMessage)
        {
            Details = details;
        }


        public string Details { get; set; }
        
        
    }
}
