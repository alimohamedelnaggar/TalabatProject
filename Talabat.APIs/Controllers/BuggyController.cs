using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.APIs.Error;
using Talabat.Repository.Data.Contexts;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly TalabatDbContext dbContext;

        public BuggyController(TalabatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("notfound")]
        public async Task<IActionResult> GetNotFound()
        {
           var brand=await dbContext.ProductBrands.FindAsync(100);
            if(brand is null)
            return NotFound(new ApiErrorResponse(404,"brand with id : 100 isn not found"));
            return Ok(brand);
        }
        [HttpGet("servererror")]
        public async Task<IActionResult> GetServerErrorAsync()
        {
            var brand = await dbContext.ProductBrands.FindAsync(100);
            var toString=brand.ToString();
            return Ok(brand);
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationError(int id)
        {
            return Ok();
        }
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedError(int id)
        {
            return Unauthorized(new ApiErrorResponse(401));
        }
    }
}
