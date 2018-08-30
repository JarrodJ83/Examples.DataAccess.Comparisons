using System;

namespace Logging
{
    public interface ILogger
    {
        void Verbose(string message);
        void Exception(Exception ex, string message);
    }

    public class Logger : ILogger
    {
        private readonly Serilog.ILogger _serilog;

        public Logger(Serilog.ILogger serilog)
        {
            _serilog = serilog;
        }
        public void Verbose(string message)
        {
            _serilog.Verbose(message);
        }

        public void Exception(Exception ex, string message)
        {
            _serilog.Error(ex, message);
        }
    }
}
