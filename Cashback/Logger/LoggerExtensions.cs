using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Cashback.Logger
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory, Func<string, LogLevel, bool> filter = null)
        {
            factory.AddProvider(new LoggerProvider(filter));
            return factory;
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel)
        {
            return AddContext(
                factory,
                (_, logLevel) => logLevel >= minLevel);
        }
    }
}
