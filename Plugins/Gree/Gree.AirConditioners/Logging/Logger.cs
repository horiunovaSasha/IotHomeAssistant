using Microsoft.Extensions.Logging;

namespace Gree.AirConditioner.Logging
{
    internal static class Logger
    {
        private static ILoggerFactory loggerFactory = new LoggerFactory();

        static Logger()
        {
            loggerFactory.AddProvider(new ConsoleLoggerProvider());
        }

        public static ILogger CreateDefaultLogger()
        {
            return loggerFactory.CreateLogger("GreeLogger");
        }

        public static ILogger CreateLogger(string category)
        {
            return loggerFactory.CreateLogger(category);
        }

        public static ILogger<T> CreateLogger<T>()
        {
            return loggerFactory.CreateLogger<T>();
        }
    }
}
