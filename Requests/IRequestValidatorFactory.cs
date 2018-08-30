using System.Collections.Generic;

namespace Requests
{
    public interface IRequestValidatorFactory<TRequest> where TRequest : IRequest
    {
        List<IRequestValidator<TRequest>> GetValidators();
    }
}
