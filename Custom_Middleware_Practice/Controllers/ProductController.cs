
using Custom_Middleware_Practice.Response;
using Microsoft.AspNetCore.Mvc;

namespace Custom_Middleware_Practice.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductClient");
        }


        [HttpGet("{id}")]
        public IActionResult DivideMe(int id)
        {
            // Force an exception (Divide by Zero)
            int result = 10 / id; // If id = 0, this will throw an exception

            return Ok($"Product ID: {id}, Result: {result}");
        }

        [HttpGet("Product")]
     
        public async Task<IActionResult> GetProducts()
        {
            
            var response = await _httpClient.GetAsync("products");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<List<JsonResponse>>();
                return Ok(products);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error fetching products");
            }
        }

    }
}
