using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cashback.Data.Context;
using Cashback.Data.Repository;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cashback.Logger
{
    public class Logger : ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private LoggerRepository _loggerRepository;
        private bool _selfException = false;

        public Logger(string categoryName, Func<string, LogLevel, bool> filter)
        {
            _categoryName = categoryName;
            _filter = filter;
            _loggerRepository = new LoggerRepository();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (_selfException)
            {
                _selfException = false;
                return;
            }
            _selfException = true;
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (exception != null)
            {
                message += "\n" + exception.ToString();
            }
            var maxMessageLength = GetMaxMessageLength();
            message = maxMessageLength != null && message.Length > maxMessageLength ? message.Substring(0, (int)maxMessageLength) : message;
            var eventLog = new LoggerApp()
                           {
                               Message = message,
                               LogLevel = logLevel.ToString(),
                               CreatedTime = DateTime.UtcNow
                           };
            try
            {
                _loggerRepository.Insert(eventLog);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        private int? GetMaxMessageLength()
        {
            int? maxLength = null;
            PropertyInfo[] props = typeof(EventLog).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    MaxLengthAttribute maxLengthAttr = attr as MaxLengthAttribute;
                    if (maxLengthAttr != null && prop.Name.Equals("Message"))
                    {
                        maxLength = maxLengthAttr.Length;
                    }
                }
            }

            return maxLength;
        }
    }
}
