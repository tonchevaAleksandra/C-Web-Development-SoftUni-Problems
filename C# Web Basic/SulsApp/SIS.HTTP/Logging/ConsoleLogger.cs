﻿using System;
using System.Globalization;

namespace SIS.HTTP.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now.ToString()}] {message}");
        }
    }
}