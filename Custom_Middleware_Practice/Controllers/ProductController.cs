
using Custom_Middleware_Practice.Filters;
using Custom_Middleware_Practice.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Custom_Middleware_Practice.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public ProductController(IHttpClientFactory httpClientFactory,IMemoryCache cache)
        {
            _httpClient = httpClientFactory.CreateClient("ProductClient");
            _cache = cache;
        }


        [HttpGet("{id}")]
        public IActionResult DivideMe(int id)
        {
            // Force an exception (Divide by Zero)
            int result = 10 / id; // If id = 0, this will throw an exception

            return Ok($"Product ID: {id}, Result: {result}");
        }

        [HttpGet("Product")]
      //Implemented In-Mermory caching
        public async Task<IActionResult> GetProducts()
        {
            if (!_cache.TryGetValue("products", out List<Product_JsonResponse> products))
            {
                var response = await _httpClient.GetAsync("products");
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode);

                products = await response.Content.ReadFromJsonAsync<List<Product_JsonResponse>>();

                _cache.Set("products", products, TimeSpan.FromSeconds(30));
            }

            return Ok(products);

        }

        [HttpGet("Posts")]
        [ServiceFilter(typeof(MemoryCacheFilter))]
        //Implemented Custom Filter caching
        public async Task<IActionResult> GetPosts()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var products = await response.Content.ReadFromJsonAsync<List<Post_JsonResponse>>();

            return Ok(products);

        }

    }
}
