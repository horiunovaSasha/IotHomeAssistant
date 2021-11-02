using Microsoft.Extensions.Logging;

namespace Gree.AirConditioner.Logging
{
    internal class ConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ConsoleLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }
}
