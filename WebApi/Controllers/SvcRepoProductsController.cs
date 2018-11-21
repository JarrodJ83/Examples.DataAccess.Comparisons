using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SvcRepoProductsController : ControllerBase
    {
        private readonly ProductService _productsService;

        public SvcRepoProductsController()
        {
            _productsService = new ProductService();
        }
        
        [HttpGet]
        public async Task<ActionResult<PagedData<Product>>> Get([FromQuery]int pageSize, [FromQuery]int offset)
        {
            return await _productsService.GetAllProductsPagedAsync(offset, pageSize);
        }

        // GET api/SvcRepoProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await _productsService.GetProductAsync(id);
        }

        // POST api/SvcRepoProducts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/SvcRepoProducts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/SvcRepoProducts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
