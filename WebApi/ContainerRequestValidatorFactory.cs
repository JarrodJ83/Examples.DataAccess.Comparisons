using System;
using System.Collections.Generic;
using System.Linq;
using Requests;
using SimpleInjector;

namespace WebApi
{
    public class ContainerRequestValidatorFactory<TRequest> : IRequestValidatorFactory<TRequest> where TRequest : IRequest
    {
        private readonly Container _container;

        public ContainerRequestValidatorFactory(Container container)
        {
            _container = container;
        }
        public List<IRequestValidator<TRequest>> GetValidators()
        {
            return _container.GetAllInstances<IRequestValidator<TRequest>>().ToList();
        }
    }
}
