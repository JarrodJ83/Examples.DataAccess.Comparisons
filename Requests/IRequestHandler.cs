using System.Threading;
using System.Threading.Tasks;

namespace Requests
{
    /// <summary>
    /// Generic interface for handling API requests that do not require a response
    /// </summary>
    /// <typeparam name="TRequest">Type of request to handle</typeparam>
    public interface IRequestHandler<in TRequest> where TRequest : IRequest
    {
        Task HandleAsync(TRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }

    /// <summary>
    /// Generic interface for handling API requests that require a response
    /// </summary>
    /// <typeparam name="TRequest">Type of request to handle</typeparam>
    /// <typeparam name="TResponse">Type of response expected which matches the generic type of <see cref="Request{TResponse}"/></typeparam>
    public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest reques, CancellationToken cancellationToken = default(CancellationToken));
    }
}