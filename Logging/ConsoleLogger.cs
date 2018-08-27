using System;
using System.Net.Http;

namespace Logging
{
    public static class ConsoleLogger
    {
        public static async void Verbose(string message)
        {
            Console.WriteLine($"[VRB]: {message}");
        }

        public static async void Exception(Exception ex, string message)
        {
            Console.WriteLine($"[VRB]: {message}\n{ex}");
        }
    }
}
