using System;
using System.Threading;
using System.Threading.Tasks;
using DomainModel;
using Requests;

namespace RequestHandlers
{
    class GetAllProductsPaged : IRequestHandler<Requests.GetAllProductsPaged, PagedData<Product>>
    {
        public GetAllProductsPaged()
        {
            
        }

        public async Task<PagedData<Product>> HandleAsync(Requests.GetAllProductsPaged request, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
