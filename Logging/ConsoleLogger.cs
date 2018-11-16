using System;
using System.Net.Http;

namespace Logging
{
    public static class ConsoleLogger
    {
        public static void Verbose(string message)
        {
            Console.WriteLine($"[VRB]: {message}");
        }

        public static void Exception(Exception ex, string message)
        {
            Console.WriteLine($"[VRB]: {message}\n{ex}");
        }
    }
}
