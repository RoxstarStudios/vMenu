using System;
using System.Collections.Generic;
using System.Text;

using CitizenFX.Core;

namespace vMenu.Server.Services
{
    internal class LoggingService
    {
        private static readonly object _padlock = new();
        private static LoggingService _instance;

        private LoggingService()
        {
            Debug.WriteLine("[vMenu] Initializing Logging Service");
        }

        public void Info(string message)
        {
            Debug.WriteLine($"^7[^6vMenu^7] ^5Info ^7- {DateTime.Now} - ^5{message}^7");
        }

        public void Error(string message)
        {
            Debug.WriteLine($"^7[^6vMenu^7] ^8Error ^7- {DateTime.Now} - ^8{message}^7");
        }

        public void Success(string message)
        {
            Debug.WriteLine($"^7[^6vMenu^7] ^2Success ^7- {DateTime.Now} - ^2{message}^7");
        }

        public void Warning(string message)
        {
            Debug.WriteLine($"^7[^6vMenu^7] ^3Warning ^7- {DateTime.Now} - ^3{message}^7");
        }

        internal static LoggingService Instance
        {
            get
            {
                lock (_padlock)
                {
                    return _instance ??= new LoggingService();
                }
            }
        }
    }
}
