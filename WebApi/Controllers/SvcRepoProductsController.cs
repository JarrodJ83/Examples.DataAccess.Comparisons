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
        private readonly IProductService _productsService;

        public SvcRepoProductsController(IProductService productsService)
        {
            _productsService = productsService;
        }

        // GET api/SvcRepoProducts

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> Get()
        //{
        //    return await _getAllProductsPaged.HandleAsync(new Requests.GetAllProductsPaged
        //    {
        //        Offset = offset,
        //        PageSize = pageSize
        //    });
        //}

        #region Paged

        [HttpGet]
        public async Task<ActionResult<PagedData<Product>>> Get([FromQuery]int pageSize, [FromQuery]int offset)
        {
            return await _productsService.GetAllProductsPagedAsync(offset, pageSize);
        }

        #endregion

        // GET api/SvcRepoProducts/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
