using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class MaxPageSizeExceededException : Exception
    {
        public MaxPageSizeExceededException(int maxPageSize)
            : base($"Max PageSize of {maxPageSize} exceeded")

        {
            
        }
    }
}
