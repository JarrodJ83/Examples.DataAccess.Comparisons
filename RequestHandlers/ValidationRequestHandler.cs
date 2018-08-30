using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Requests;

namespace RequestHandlers
{
    public class ValidationRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _requestHandler;
        private readonly IRequestValidatorFactory<TRequest> _requestValidationFactory;

        public ValidationRequestHandler(IRequestHandler<TRequest, TResponse> requestHandler, IRequestValidatorFactory<TRequest> requestValidationFactory)
        {
            _requestHandler = requestHandler;
            _requestValidationFactory = requestValidationFactory;
        }

        public async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<IRequestValidator<TRequest>> requestValidators = _requestValidationFactory.GetValidators();

            foreach (IRequestValidator<TRequest> requestValidator in requestValidators)
            {
                RequestValidationResult result = requestValidator.Validate(request);

                if (!result.Successful)
                {
                    throw new AggregateException(result.FailureReasons.Select(failure => new Exception(failure)));
                }
            }

            return await _requestHandler.HandleAsync(request, cancellationToken);
        }
    }
}
