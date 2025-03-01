using Microsoft.AspNetCore.Mvc;

namespace Custom_Middleware_Practice.Controllers
{
    public class ProductController : ControllerBase
    {


        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = Guid.NewGuid().ToString();
            if (product != null)
                return NotFound("Product not found");

           return Ok("Hellow Wold");
        }

    }
}
