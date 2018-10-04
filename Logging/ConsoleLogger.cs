using System;
using System.Net.Http;

namespace Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Verbose(string message)
        {
            Console.WriteLine($"[VRB]: {message}");
        }

        public void Exception(Exception ex, string message)
        {
            Console.WriteLine($"[VRB]: {message}\n{ex}");
        }
    }
}
