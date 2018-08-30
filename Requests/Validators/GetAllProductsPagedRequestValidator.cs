using System;
using System.Collections.Generic;
using System.Linq;

namespace Requests.Validators
{
    class GetAllProductsPagedRequestValidator : IRequestValidator<Requests.GetAllProductsPagedRequest>

    {
        private const int _maxPageSize = 100;

        public RequestValidationResult Validate(GetAllProductsPagedRequest model)
        {
            List<string> failures = new List<string>();

            if (model.PageSize > _maxPageSize)
            {
                failures.Add($"{nameof(model.PageSize)} must be less than or equal to {_maxPageSize}");
            }

            return failures.Any() ? 
                RequestValidationResult.Failed(failures) : 
                RequestValidationResult.Passed();
        }
    }
}
