using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Requests;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CqrsProductsController : ControllerBase
    {
        private readonly IRequestHandler<Requests.GetAllProductsPagedRequest, PagedData<Product>> _getAllProductsPaged;

        public CqrsProductsController(
            IRequestHandler<Requests.GetAllProductsPagedRequest, PagedData<Product>> getAllProductsPaged
            )
        {
            _getAllProductsPaged = getAllProductsPaged;
        }

        // GET api/CQRSProducts

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
            return await _getAllProductsPaged.HandleAsync(
                new Requests.GetAllProductsPagedRequest(offset, pageSize));
        }

        #endregion

        // GET api/CQRSProducts/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/CQRSProducts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/CQRSProducts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/CQRSProducts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
