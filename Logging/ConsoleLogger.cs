using System;
using System.Diagnostics;
using System.Net.Http;

namespace Logging
{
    public interface ILogger
    {
        void Verbose(string message);
        void Exception(Exception ex, string message);
    }

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
