using System;

namespace Logging
{
    public class SerilogLogger : ILogger
    {
        private readonly Serilog.ILogger _serilog;

        public SerilogLogger(Serilog.ILogger serilog)
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
