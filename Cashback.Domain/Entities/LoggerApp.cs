using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.Entities
{
    public class LoggerApp
    {
        public long Id { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
