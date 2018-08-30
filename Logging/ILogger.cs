using System;

namespace Logging
{
    public interface ILogger
    {
        void Verbose(string message);
        void Exception(Exception ex, string message);
    }
}
