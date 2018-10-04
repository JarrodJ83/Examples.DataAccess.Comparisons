using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public interface ILogger
    {
        void Verbose(string message);
        void Exception(Exception ex, string message);
    }
}
