using Talabat.APIs.Error;

namespace Talabat.APIs.Errors
{
    public class ValidationErrorResponse : ApiErrorResponse
    {

        public IEnumerable<string> Errors { get; set; }= new List<string>();


        public ValidationErrorResponse() : base(400)
        {
        }
    }
}
