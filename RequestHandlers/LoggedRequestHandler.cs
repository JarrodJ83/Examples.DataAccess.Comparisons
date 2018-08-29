using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Logging;
using Requests;

namespace RequestHandlers
{
    public class LoggedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;
        private readonly IRequestHandler<TRequest, TResponse> _requestHandler;

        public LoggedRequestHandler(ILogger logger, IRequestHandler<TRequest, TResponse> requestHandler)
        {
            _logger = logger;
            _requestHandler = requestHandler;
        }

        public async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            _logger.Verbose($"Handling {typeof(TRequest).FullName} request");

            try
            {
                return await _requestHandler.HandleAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Exception(e, $"Error handling {typeof(TRequest).FullName} request");

                throw;
            }
        }
    }
}
