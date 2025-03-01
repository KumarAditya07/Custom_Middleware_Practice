using Custom_Middleware_Practice.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Custom_Middleware_Practice.Controllers
{
    public class ProductController : ControllerBase
    {


        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            // Force an exception (Divide by Zero)
            int result = 10 / id; // If id = 0, this will throw an exception

            return Ok($"Product ID: {id}, Result: {result}");
        }

        [HttpGet("secure-data")]
        [AuthFilter]
        public IActionResult GetSecureData()
        {
            return Ok("This is secured data for admins!");
        }

    }
}
