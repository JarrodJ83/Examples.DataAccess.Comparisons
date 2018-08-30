using System.Collections.Generic;
using System.Linq;

namespace Requests
{
    public class RequestValidationResult
    {
        public bool Successful => !FailureReasons.Any();
        public List<string> FailureReasons { get; }

        public RequestValidationResult(List<string> failureReasons = null)
        {
            FailureReasons = failureReasons ?? new List<string>();
        }

        public static RequestValidationResult Passed()
        {
            return new RequestValidationResult();
        }

        public static RequestValidationResult Failed(List<string> failureReasons)
        {
            return new RequestValidationResult(failureReasons);
        }
    }
}
