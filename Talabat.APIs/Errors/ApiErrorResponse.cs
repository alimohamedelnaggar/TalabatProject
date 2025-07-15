namespace Talabat.APIs.Error
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }  

        public ApiErrorResponse(int statusCode, string? errorMessage=null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetDefaultMessage(statusCode) ;
        }
        
        private string GetDefaultMessage(int statusCode)
        {
            var errorMessage = statusCode switch
            {
                400=>"a bad request , you have made",
                401=>"authorized, you r not",
                404=>"not fount",
                500=>"server error",
                _=>null

            };

            return errorMessage;
        }
    }
}
